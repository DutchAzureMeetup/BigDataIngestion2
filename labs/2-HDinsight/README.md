# Process data with Spark

## Preparation

We have prepared this lab by setting up a HDInsigh-cluster. This is an Hortonworks distribution of Hadoop in the Azure-cloud.

The cluster exists of 2 master and a couple of worker-nodes. The CPU and memory of the workers will be shared trough YARN (YetAnotherResourceNegotiator). Later in this lab we will be able to view the resource consumption of the sessions on the cluster.

When you start a Spark-session on the cluster you will claim some of there resources.

![HDinsightSpark_architecture](img/hdispark.architecture.png)

More information about HDinsight & spark on the Microsoft website: [hdinsight-apache-spark-overview](https://docs.microsoft.com/en-us/azure/hdinsight/hdinsight-apache-spark-overview)


## Jupyter - interactive notebook

For this lab we will be using an interactive notebook in the webbrowser to explore your generated data. 

### Goal
Goal of this lab is to show the possibilities of Jupyter to code interactivly and touch the open-source world of Hadoop.

### Out of the box
HDinsigh comes with a Jupyter-notebook which has nice examples and getting started guides, but is only accessible with the master username / password. This exposed at https://[CLUSTERNAME].azurehdinsight.net/jupyter). Not suiteable for multiple users.

If you want to try it for yourself feel you can follow the Azure Microsoft tutorial: [hdinsight-apache-spark-jupyter-spark-sql](https://docs.microsoft.com/en-us/azure/hdinsight/hdinsight-apache-spark-jupyter-spark-sql)

### Prepared for you

For this lab we have created a extra gateway node to work with multiple users on Jupyter (using [JupyterHub](https://jupyterhub.readthedocs.io/en/latest/)). This instance does not have all the nice features of the MS-Jupyter notebook, but is good enough for this lab.

You can access a personal Jupyter instance per user:

https://meetup-custom-gateway.westeurope.cloudapp.azure.com:8000/user/[username]

You can login with a predefined password.

### Create a new Notebook

- First we need to create an new notebook:

![](img/create_new_notebook.png)

You should give your notebook a meaningfull name and save it. You can see which kernel you are using. The Python 3.5 kernel is installed and managed by Conda in our case. You can install many kernels; for Scala, R, Python2/3, PySpark and other languages. 

On the HDInsight-cluster there are some more kernels prepared. We have prepared only this kernel which is suiteable for Spark coded in Python.

When the kernel is working you will see the white-circle being black. And the cell has an astrix [*] on the left.
 
![](img/empty_notebook.png)

### Access data with HDFS

You can execute shell (OS-commands) when prefix with '!'.

You can interact with HDFS-api to access your storage account. For this to work you need to add you credentials in the Hadoop configuration on the system.

```
!hdfs dfs -ls wasbs://[CONTAINER]@[STORAGEACCOUNT].blob.core.windows.net/

# recursive list all files, only first 10 items:
!hdfs dfs -ls -R 'wasbs://15minutes@bdi24dev.blob.core.windows.net/' | head -n10

# Look at the content of a file:
hdfs dfs -cat wasbs://15minutes@bdi24dev.blob.core.windows.net/bdi24-eh-dev/dambd/0/2017/02/19/10/22/50 | head -n 2

# First bytes show its Avro, with the schema. The rest is data
```

Lets see how 'big' our dataset is:

```
!hdfs dfs -du -h 'wasbs://15minutes@bdi24dev.blob.core.windows.net/bdi24-eh-dev/dambd/0/'
```

### Start a Spark Session

We will code in python and import findspark.
docs: [SparkSession](http://spark.apache.org/docs/latest/api/python/pyspark.sql.html#pyspark.sql.SparkSession)

```
import findspark
findspark.init(spark_home='/usr/hdp/current/spark2-client/',
               python_path='/usr/bin/anaconda/envs/py35/bin/python')

from pyspark.sql import SparkSession

spark = (SparkSession
      .builder
# Optional add/override default config
#     .config("spark.jars.packages","com.databricks:spark-avro_2.11:3.2.0")
      .appName("User1Session")
      .getOrCreate())

```

If you are done you might stop your session to not use too many resources on the cluster 
```spark.stop()```

Because the filenames do not and on avro we need to set a specific hadoop option:

```
spark.sparkContext._jsc.hadoopConfiguration().set('avro.mapred.ignore.inputs.without.extension', 'false')

```

Now we are able to load our data is a Spark DataFrame (in memory | column oriented data scructure) docs: [spark-avro](https://github.com/databricks/spark-avro#python-api)

```
wasb_url = 'wasbs://15minutes@bdi24dev.blob.core.windows.net'
folders = '/bdi24-eh-dev/dambd/0/2017/*/*/*/*/*'
input_df = spark.read.format("com.databricks.spark.avro") \
					.load(wasb_url + folders)

input_df.show()
input_df.count()
```
The count() function wil actually load all the data and count all the rows available.

You might have some data in your DataFrame, like me:

![loaded_input_df](img/notebook_input_df.png)

The headers are present, but we have to convert the Body, because it looks like a byte-array. We store this in a new DataFrame called `meter_data_df`. This was a bit of a challange and there might be better ways, but we managed to transform the Boty field into a usefull DataFrame. Docs: [spark.functions](http://spark.apache.org/docs/latest/api/python/pyspark.sql.html#module-pyspark.sql.functions) 

```
from pyspark.sql import functions as F

meter_data_df = meter_data_df\
    .withColumn("Body", F.col("Body").astype('string'))\
    .withColumn("Datetime", F.get_json_object('Body',"$.Date").astype('timestamp'))\
    .withColumn("ElectricityUsage", F.get_json_object('Body',"$.ElectricityUsage").astype('integer'))\
    .withColumn("CustomerId", F.get_json_object('Body',"$.CustomerId"))\
    .withColumn("Date", F.date_format("Datetime", "yyyy-MM-dd"))\
    .drop("Body")
    
meter_data_df.persist()
meter_data_df.count()
```

...

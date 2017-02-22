# Big Data Ingestion Part 2
## Create a Data Factory  

Let's create our first copy activity in Data Factory!

In this lab we will copy all the data saved in the archive container (you did this in the [preparation](https://github.com/DutchAzureMeetup/BigDataIngestion2/tree/master/labs/0-Preparation) lab) to the HDInsight cluster storage.   


1. Go the Resource Group created during the preparation lab:

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/1.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/2.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/3.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/4.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/5.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/6.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/7.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/8.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/9.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/10.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/11.png)

**You will need this values for the next step:** 

| Kind | Value |
| -------------------- | ------------------ |
| **Storage Account Name:** | pn123dev | 
| **Storage Account Key:**  | kj47gsDyOSMEPu3QGfAvxWOBDpBZ/9U/xrnqfP5uwaK2N5hauoVzW4yLxRdaXabYOZaSEgiNYr0/s4FOaOhXww== |

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/12.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/13.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/14.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/15.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/16.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/17.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/18.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/19.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/20.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/1-DataFactory/img/21.png)

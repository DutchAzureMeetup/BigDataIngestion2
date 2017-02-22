# Big Data Ingestion Part 2

## Let's start where we left off:

1. Click this button (hold CTRL while clicking to open in a new tab):

    <a target="_blank" id="deploy-to-azure"  href="https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FDutchAzureMeetup%2FBigDataIngestion1%2Fmaster%2Fsrc%2FAzureInfrastructure%2Fazuredeploy.json"><img src="http://azuredeploy.net/deploybutton.png"/></a>

2. Fill the required settings:

    ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion1/master/img/intro.png)

3. Wait until the deployment finish

## Enable the Azure Event Hubs Archive 

4. Go to the newly crated Resource Group and select the Event Hub:

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04eventhubnamespaceselect.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04eventhubselect.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04eventhubpropertiesselect.png)
  
   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04eventhuarchivesetuppng.png)
  
   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04storageselect.png)
 
   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04createcontainer1.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04createcontainer2.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04createcontainer3.png)

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04createcontainer4.png)

### The Azure Event Hubs Archive will now create a backup in [AVRO](http://avro.apache.org/docs/current/) format every 15 minutes in the storage account.

The archive service saves the blobs using the following structure in the storage account container (named archive) we have created:

* eventhub namespace
    * eventhub name
        * partition # (in our case 0 and 1)         
            * year
                * month
                    * day
                        * hour
                            * minutes (every 1 minute) -> here in is the AVRO file blob
                        
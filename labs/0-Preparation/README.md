# Big Data Ingestion Part 2

In the previous Meetup we were using Stream Analytics to save data from the Event Hub to an Azure blob storage account. 
But there's also another way to accomplish the same, a new feauture called [Event Hub Archive](https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-archive-overview). 
We will use this feature because it's able to save the data more often (1 minute) than Stream Analytics (1 hour), which is useful for this Meetup to save data faster. 

## Goal
Enable the Event Hub Archive service to automatically saves every minute the Event Hub messages in an Azure blob storage.  

## Let's start where we left off:

### **Step 1**
Click this button (hold CTRL while clicking to open in a new tab):

<a target="_blank" id="deploy-to-azure"  href="https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FDutchAzureMeetup%2FBigDataIngestion1%2Fmaster%2Fsrc%2FAzureInfrastructure%2Fazuredeploy.json"><img src="http://azuredeploy.net/deploybutton.png"/></a>

### **Step 2**
Fill the required settings:

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion1/master/img/intro.png)

### **Step 3**
Wait until the deployment finishes (it should take no more than 10 minutes).

## Enable the Azure Event Hubs Archive 

### **Step 4**
Go to the newly crated Resource Group and select the Event Hub:

   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04eventhubnamespaceselect.png)

### **Step 5**
   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04eventhubselect.png)

### **Step 6**
   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04eventhubpropertiesselect.png)

### **Step 7**
   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04eventhuarchivesetuppng.png)

### **Step 8**
   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04storageselect.png)

### **Step 9**
   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04createcontainer1.png)

### **Step 10**
   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04createcontainer2.png)

### **Step 11**
   ![](https://raw.githubusercontent.com/DutchAzureMeetup/BigDataIngestion2/master/labs/0-Preparation/img/04createcontainer3.png)

### **Step 12**
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
                        

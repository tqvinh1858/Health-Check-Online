
# Title

"BHEP - Better Health for Each Person" is a startup project that Raptor team is in the process of developing and implementing. With the goal of providing everyone with good health care services without having to go to the hospital. Creating convenience for users with health monitoring features and direct communication with Doctors at a corresponding price, Raptor team aims to become a reliable health service provider and constantly improve quality.

## Constructure Code

The Code base following principles Clean Architecture and CQRS pattern.

![Clean-Architecture](Image/Clean-Architecture.png)

![CQRS](Image/CQRS.jpg)

Transfer internal data using `MediatR` and external use `RabbitMQ` / `Azure ServiceBus`.

## Deployment

### Azure
- [Serivce App](https://learn.microsoft.com/en-us/azure/app-service): deploy API 
- [BlobStorage](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blobs-overview): store Images and Videos
- [Azure Cache for Redis](https://learn.microsoft.com/en-us/azure/azure-cache-for-redis): caching data from Query
- [ServiceBus](https://learn.microsoft.com/en-us/azure/service-bus-messaging): using queue and topic/subscription for handle 

### Payment 
- [VNPay](https://sandbox.vnpayment.vn/apis/docs/gioi-thieu)
- [PayOS](https://payos.vn/docs/api)

### Other

In Dev Enviroment use `RabbitMQ` and `Redis` deploy in docker to reduce costs

To deploy in Docker

- First you type `dir` it will display information about all files and subfolders in the current directory

```powershell
Mode                 LastWriteTime         Length Name                                                                                                                                                                                    
----                 -------------         ------ ----                                                                                                                                                                                    
d-----          5/7/2024   9:29 PM                .github                                                                                                                                                                                 
d-----         6/29/2024   1:31 PM                SpiritArduino                                                                                                                                                                           
d-----          8/2/2024   1:31 PM                Src                                                                                                                                                                                     
d-----          5/7/2024   9:29 PM                Test                                                                                                                                                                                    
-a----          5/7/2024   9:29 PM          14866 .editorconfig                                                                                                                                                                           
-a----          5/7/2024   9:29 PM           7256 .gitignore                                                                                                                                                                              
-a----          8/2/2024   3:05 PM          10292 BHEP.sln                                                                                                                                                                                
-a----         7/30/2024   5:51 PM            458 docker-compose.Dev.Infrastructure.yaml                                                                                                                                                  
-a----          5/7/2024   9:29 PM             11 README.md                                                                                                                                                                               

```
- Then u copy the name of file <docker.yaml> and write command below

````powershell
docker compose -f <docker.yaml> up
````

- Finnaly wait to docker deploy ....

## Authentication

JWT Token to Authentication

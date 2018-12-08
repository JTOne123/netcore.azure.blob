# netcore.azure.blob
Service for azure blob storage

- Add nuget package `NetCore.Azure.Blob`

- Add servicee using `services.AddAzureBlob(config => ...)`.

  Alternatively, To map directly to secret manager, use the following syntax:
  
  ```
  "azBlob": {
    "key": "",
    "keyName": "",
    "accountName": ""
  }
  ```
  
  Followed by the following in the startup 
  
  ```services.AddAzureBlob(Configuration.GetSection("azBlob"));```

- Access blob service with DI using `IBlobManager`

# MessagingBus
MessageBus.Abstraction is a lightweight and extensible .NET library that defines a generic interface for message/event publishing across different message brokers

### URL
https://3efawigrphjfmnxe243s33gwki0kbtbl.lambda-url.us-east-1.on.aws/swagger/index.html

---

``` json
{
  "accountId": "ACC-001",
  "personType": "PF",
  "holder": {
    "name": "Ana Paula",
    "taxId": "123.456.789-00",
    "documents": {
      "idCard": {
        "number": "55.444.333-2",
        "issuer": "SSP-SP"
      },
      "driverLicense": {
        "number": "99999999999",
        "category": "B",
        "expiration": "2028-12-31"
      }
    }
  },
  "settings": {
    "preferences": {
      "notifications": {
        "email": true,
        "sms": false,
        "push": {
          "android": true,
          "ios": false
        }
      },
      "language": "en-US"
    },
    "security": {
      "twoFactorAuth": true,
      "recentLogins": [
        {
          "date": "2025-06-01T09:15:00Z",
          "ip": "192.168.0.10",
          "location": {
            "country": "Brazil",
            "city": "São Paulo"
          }
        },
        {
          "date": "2025-05-28T21:45:00Z",
          "ip": "192.168.0.15",
          "location": {
            "country": "Brazil",
            "city": "Campinas"
          }
        }
      ]
    }
  },
  "relationships": {
    "dependents": [
      {
        "name": "Lucas",
        "age": 12,
        "schoolHistory": {
          "current": {
            "grade": "7th grade",
            "school": {
              "name": "Central School",
              "address": {
                "street": "Education Street, 45",
                "neighborhood": "Downtown",
                "city": "São Paulo"
              }
            }
          },
          "previous": [
            {
              "grade": "6th grade",
              "school": "Future School"
            }
          ]
        }
      },
      {
        "name": "Marina",
        "age": 14,
        "schoolHistory": {
          "current": {
            "grade": "9th grade",
            "school": {
              "name": "Newton High School",
              "address": {
                "street": "Science Avenue, 900",
                "neighborhood": "Tech District",
                "city": "Campinas"
              }
            }
          },
          "previous": [
            {
              "grade": "8th grade",
              "school": "Galileo School"
            },
            {
              "grade": "7th grade",
              "school": "Galileo School"
            }
          ]
        }
      }
    ]
  }
}

```

# Comparison


![image](https://github.com/user-attachments/assets/ad8561a1-618e-41aa-9993-5b2211d019ca)
|:--:| 
| *https://stackoverflow.com/questions/28687295/sqs-vs-rabbitmq* |

![image](https://github.com/user-attachments/assets/ed256d2d-b01a-4b92-87f6-be1815d02ea7)
|:--:| 
| *https://www.geeksforgeeks.org/difference-between-rabbitmq-vs-sqs* |

![image](https://github.com/user-attachments/assets/bbfbb2a2-0c2c-411b-abcd-5d97c7dd153e)
|:--:| 
| *https://www.geeksforgeeks.org/difference-between-rabbitmq-vs-sqs* |

![image](https://github.com/user-attachments/assets/5d4fe52b-0fe5-4ef3-ae57-450122f74441)
|:--:|
| *https://www.svix.com/resources/faq/rabbitmq-vs-sqs/* | 

![image](https://github.com/user-attachments/assets/cf156fad-4485-4e22-b362-656e820b899c)
|:--:|
| *https://www.svix.com/resources/faq/rabbitmq-vs-sqs/* |

![image](https://github.com/user-attachments/assets/76556122-1699-4fcb-9559-5601afe0cca4)
|:--:|
| *https://www.svix.com/resources/faq/rabbitmq-vs-sqs/* |

# References


https://www.jsongenerator.io/

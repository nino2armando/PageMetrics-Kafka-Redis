PageMetrics + Apache Kafka + Redis
===========
![alt tag](https://github.com/nino2armando/PageMetrics/blob/master/etc/IMG_20140813_141139.jpg)


Kafka commands
===========
```
bin\windows\zookeeper-server-start.bat config\zookeeper.properties
bin\windows\kafka-server-start.bat config\server.properties
bin\windows\kafka-console-consumer.bat --zookeeper localhost:2181 --topic PageLoadTime --from-beginning
kafka-console-producer.bat --broker-list localhost:9092 --topic PageLoadTime
```

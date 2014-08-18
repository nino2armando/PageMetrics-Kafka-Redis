PageMetrics + Apache Kafka + Redis
===========
![alt tag](https://github.com/nino2armando/PageMetrics/blob/master/etc/IMG_20140813_141139.jpg)


Metrics RoadMap
===========
- We can introduce other metric types:
        - Counters --> count any type of event ('some.custom.events',10
        - Timers --> how long something took
        - Sets --> count of unique values passed to a key

Kafka commands
===========
```
bin\windows\zookeeper-server-start.bat config\zookeeper.properties
bin\windows\kafka-server-start.bat config\server.properties
bin\windows\kafka-console-consumer.bat --zookeeper localhost:2181 --topic PageLoadTime --from-beginning
kafka-console-producer.bat --broker-list localhost:9092 --topic PageLoadTime
```

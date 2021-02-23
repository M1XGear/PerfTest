# PerfTest

* [En](#en)
  * [Task description](#task-description)
* [Ru](#ru)
  * [Описание задачи](#описание-задачи)
* [Results](#results)
* [Raw data](#raw-data)

## En
### Task description
Where is finite input of integer numbers. One number is given per unit of time.
Program need to save this input and be able to print them in ordered way any time.

## Ru
### Описание задачи:
Есть конченый набор входных данных. Каждый момент времени подаётся одно число.

Нужно сохранять эти входные данные и иметь возможность получить их в отсортированном виде в любой момент времени.

## Results
![TimePerformance](https://github.com/M1XGear/PerfTest/blob/main/Results/AllData.svg?raw=true)

.NET Core 5.0 is faster than .NET Core 3.1
![RuntimeDiff](https://github.com/M1XGear/PerfTest/blob/main/Results/RuntimeDiff.svg?raw=true)

Time performance/Values range based on input size
| * | * |
| --- | --- |
| <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Mean_InputSize_100000.png?raw=true)" height="450"> | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Mean_InputSize_1000000.png?raw=true)" height="450"> |
| <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Mean_InputSize_5000000.png?raw=true)" height="450"> | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Mean_InputSize_10000000.png?raw=true)" height="450"> |

Time performance/input size based on values range
| ValuesRange | Chart |
| --- | --- |
| 100k | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Mean_ValuesRange_100000.png?raw=true)" height="450">    |
| 10M  | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Mean_ValuesRange_10000000.png?raw=true)" height="450">  |
| 50M  | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Mean_ValuesRange_50000000.png?raw=true)" height="450">  |
| 100M | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Mean_ValuesRange_100000000.png?raw=true)" height="450"> |

Memory allocation/Values range based on input size
| InputSize | Chart |
| --- | --- |
| 100k | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Memory_InputSize_100000.png?raw=true)" height="450">   |
|  1M  | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Memory_InputSize_1000000.png?raw=true)" height="450">  |
|  5M  | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Memory_InputSize_5000000.png?raw=true)" height="450">  |
|  10M | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Memory_InputSize_10000000.png?raw=true)" height="450"> |

Memory allocation/input size based on values range
| ValuesRange | Chart |
| --- | --- |
| 100k | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Memory_ValuesRange_100000.png?raw=true)" height="450">    |
| 10M  | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Memory_ValuesRange_10000000.png?raw=true)" height="450">  |
| 50M  | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Memory_ValuesRange_50000000.png?raw=true)" height="450">  |
| 100M | <img src="https://github.com/M1XGear/PerfTest/blob/main/Results/Memory_ValuesRange_100000000.png?raw=true)" height="450"> |

# Raw data
[Data](https://github.com/M1XGear/PerfTest/blob/main/Results/PerfTestData.csv)

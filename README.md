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
![Time performance](https://github.com/M1XGear/PerfTest/blob/main/Results/Performance.svg?raw=true)


![Memory performance](https://github.com/M1XGear/PerfTest/blob/main/Results/Memory%20(.NET%20Core%205.0).svg?raw=true)

[More charts](https://docs.google.com/spreadsheets/d/1mpi4laiTy0I0kRjMOtgpowWvan4lzcZMBvnko-Ea5Ig/edit?usp=sharing)

# Raw data
``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  Core31 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  Core50 : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Platform=X64  Force=True  Server=True  
IterationCount=5  LaunchCount=1  RunStrategy=ColdStart  
UnrollFactor=1  WarmupCount=1  

```
|       Runtime | InputSize |             Consumer |         Mean |       Error |       StdDev |     Gen 0 | Gen 1 | Gen 2 |     Allocated |
|-------------- |---------- |--------------------- |-------------:|------------:|-------------:|----------:|------:|------:|--------------:|
| **.NET Core 3.1** |    **100000** |                **Array** |    **310.14 ms** |    **63.60 ms** |    **16.516 ms** |         **-** |     **-** |     **-** |  **781767.99 KB** |
| .NET Core 5.0 |    100000 |                Array |    299.19 ms |    17.04 ms |     4.425 ms |         - |     - |     - |  781768.31 KB |
| **.NET Core 3.1** |    **100000** | **ConcurrentDictionary** |     **26.70 ms** |    **41.87 ms** |    **10.874 ms** |         **-** |     **-** |     **-** |    **7154.84 KB** |
| .NET Core 5.0 |    100000 | ConcurrentDictionary |     24.58 ms |    26.95 ms |     7.000 ms |         - |     - |     - |    7154.48 KB |
| **.NET Core 3.1** |    **100000** |           **Dictionary** |    **581.96 ms** |    **71.86 ms** |    **18.662 ms** |         **-** |     **-** |     **-** | **1172393.02 KB** |
| .NET Core 5.0 |    100000 |           Dictionary |    647.06 ms |    38.17 ms |     9.912 ms |         - |     - |     - | 1172393.27 KB |
| **.NET Core 3.1** |    **100000** |           **LinkedList** |     **22.60 ms** |    **21.86 ms** |     **5.677 ms** |         **-** |     **-** |     **-** |     **7548.9 KB** |
| .NET Core 5.0 |    100000 |           LinkedList |     20.14 ms |    13.32 ms |     3.459 ms |         - |     - |     - |    7548.68 KB |
| **.NET Core 3.1** |    **100000** |                 **List** |     **17.49 ms** |    **26.82 ms** |     **6.965 ms** |         **-** |     **-** |     **-** |    **6957.42 KB** |
| .NET Core 5.0 |    100000 |                 List |     15.03 ms |    13.03 ms |     3.384 ms |         - |     - |     - |     6957.2 KB |
| **.NET Core 3.1** |    **100000** |           **SortedList** |    **630.11 ms** |    **36.26 ms** |     **9.416 ms** |         **-** |     **-** |     **-** |  **789960.95 KB** |
| .NET Core 5.0 |    100000 |           SortedList |    613.10 ms |    51.40 ms |    13.349 ms |         - |     - |     - |  789960.68 KB |
| **.NET Core 3.1** |    **100000** |                 **Void** |     **13.27 ms** |    **22.42 ms** |     **5.823 ms** |         **-** |     **-** |     **-** |     **517.53 KB** |
| .NET Core 5.0 |    100000 |                 Void |     10.61 ms |    15.05 ms |     3.909 ms |         - |     - |     - |     517.93 KB |
| **.NET Core 3.1** |   **1000000** |                **Array** |    **573.76 ms** |    **72.53 ms** |    **18.837 ms** |         **-** |     **-** |     **-** |  **785353.42 KB** |
| .NET Core 5.0 |   1000000 |                Array |    517.32 ms |    74.79 ms |    19.424 ms |         - |     - |     - |  785358.59 KB |
| **.NET Core 3.1** |   **1000000** | **ConcurrentDictionary** |    **330.63 ms** |   **198.43 ms** |    **51.532 ms** |         **-** |     **-** |     **-** |   **70180.27 KB** |
| .NET Core 5.0 |   1000000 | ConcurrentDictionary |    329.25 ms |   122.41 ms |    31.791 ms |         - |     - |     - |    70182.5 KB |
| **.NET Core 3.1** |   **1000000** |           **Dictionary** |    **961.49 ms** |    **56.36 ms** |    **14.636 ms** |         **-** |     **-** |     **-** | **1175978.38 KB** |
| .NET Core 5.0 |   1000000 |           Dictionary |    909.90 ms |    22.55 ms |     5.857 ms |         - |     - |     - |  1175979.2 KB |
| **.NET Core 3.1** |   **1000000** |           **LinkedList** |    **176.10 ms** |    **89.93 ms** |    **23.356 ms** |         **-** |     **-** |     **-** |   **74415.46 KB** |
| .NET Core 5.0 |   1000000 |           LinkedList |    169.97 ms |    51.52 ms |    13.379 ms |         - |     - |     - |   74415.87 KB |
| **.NET Core 3.1** |   **1000000** |                 **List** |    **135.58 ms** |    **50.91 ms** |    **13.221 ms** |         **-** |     **-** |     **-** |   **27540.46 KB** |
| .NET Core 5.0 |   1000000 |                 List |    126.58 ms |    43.74 ms |    11.358 ms |         - |     - |     - |   27540.29 KB |
| **.NET Core 3.1** |   **1000000** |           **SortedList** |  **1,518.47 ms** |    **20.89 ms** |     **5.425 ms** |         **-** |     **-** |     **-** |   **850889.8 KB** |
| .NET Core 5.0 |   1000000 |           SortedList |  1,583.80 ms |    50.91 ms |    13.220 ms |         - |     - |     - |  850890.67 KB |
| **.NET Core 3.1** |   **1000000** |                 **Void** |    **102.04 ms** |    **86.92 ms** |    **22.574 ms** |         **-** |     **-** |     **-** |    **4102.84 KB** |
| .NET Core 5.0 |   1000000 |                 Void |     98.84 ms |    56.39 ms |    14.645 ms |         - |     - |     - |    2054.19 KB |
| **.NET Core 3.1** |   **5000000** |                **Array** |  **1,406.46 ms** |    **40.41 ms** |    **10.496 ms** |         **-** |     **-** |     **-** |  **814027.89 KB** |
| .NET Core 5.0 |   5000000 |                Array |  1,323.84 ms |   133.89 ms |    34.772 ms |         - |     - |     - |  814038.99 KB |
| **.NET Core 3.1** |   **5000000** | **ConcurrentDictionary** |  **2,039.38 ms** | **1,774.26 ms** |   **460.770 ms** |         **-** |     **-** |     **-** |  **356643.18 KB** |
| .NET Core 5.0 |   5000000 | ConcurrentDictionary |  1,997.04 ms | 1,187.83 ms |   308.475 ms |         - |     - |     - |  356625.23 KB |
| **.NET Core 3.1** |   **5000000** |           **Dictionary** |  **2,462.89 ms** |   **159.55 ms** |    **41.435 ms** |         **-** |     **-** |     **-** | **1204652.12 KB** |
| .NET Core 5.0 |   5000000 |           Dictionary |  2,389.00 ms |    76.53 ms |    19.875 ms |         - |     - |     - | 1204652.55 KB |
| **.NET Core 3.1** |   **5000000** |           **LinkedList** |    **818.11 ms** |   **301.60 ms** |    **78.325 ms** |         **-** |     **-** |     **-** |  **384339.05 KB** |
| .NET Core 5.0 |   5000000 |           LinkedList |    836.26 ms |   135.83 ms |    35.274 ms |         - |     - |     - |  384344.73 KB |
| **.NET Core 3.1** |   **5000000** |                 **List** |    **602.03 ms** |   **270.65 ms** |    **70.287 ms** |         **-** |     **-** |     **-** |  **149964.62 KB** |
| .NET Core 5.0 |   5000000 |                 List |    586.24 ms |   271.04 ms |    70.387 ms |         - |     - |     - |   149969.1 KB |
| **.NET Core 3.1** |   **5000000** |           **SortedList** |  **5,528.94 ms** | **1,314.60 ms** |   **341.396 ms** |         **-** |     **-** |     **-** | **1076172.26 KB** |
| .NET Core 5.0 |   5000000 |           SortedList |  6,486.17 ms | 1,682.33 ms |   436.895 ms |         - |     - |     - | 1076172.03 KB |
| **.NET Core 3.1** |   **5000000** |                 **Void** |    **574.85 ms** |   **363.80 ms** |    **94.477 ms** |         **-** |     **-** |     **-** |   **16392.82 KB** |
| .NET Core 5.0 |   5000000 |                 Void |    499.47 ms |   448.80 ms |   116.552 ms |         - |     - |     - |    8199.91 KB |
| **.NET Core 3.1** |  **10000000** |                **Array** |  **2,727.75 ms** |   **175.31 ms** |    **45.528 ms** |         **-** |     **-** |     **-** |  **846796.19 KB** |
| .NET Core 5.0 |  10000000 |                Array |  3,341.86 ms |   864.74 ms |   224.571 ms |         - |     - |     - |  846800.04 KB |
| **.NET Core 3.1** |  **10000000** | **ConcurrentDictionary** |  **6,340.56 ms** | **4,496.69 ms** | **1,167.775 ms** |         **-** |     **-** |     **-** |  **697466.16 KB** |
| .NET Core 5.0 |  10000000 | ConcurrentDictionary |  5,873.62 ms | 3,499.86 ms |   908.902 ms |         - |     - |     - |   697563.2 KB |
| **.NET Core 3.1** |  **10000000** |           **Dictionary** |  **7,026.70 ms** |   **601.75 ms** |   **156.274 ms** |         **-** |     **-** |     **-** | **1237421.17 KB** |
| .NET Core 5.0 |  10000000 |           Dictionary |  6,804.62 ms | 1,067.40 ms |   277.199 ms |         - |     - |     - | 1237420.98 KB |
| **.NET Core 3.1** |  **10000000** |           **LinkedList** |  **1,648.61 ms** |   **370.70 ms** |    **96.269 ms** | **1000.0000** |     **-** |     **-** |  **768670.55 KB** |
| .NET Core 5.0 |  10000000 |           LinkedList |  1,621.85 ms |   595.65 ms |   154.689 ms | 1000.0000 |     - |     - |  768675.08 KB |
| **.NET Core 3.1** |  **10000000** |                 **List** |  **1,215.61 ms** |   **531.40 ms** |   **138.003 ms** |         **-** |     **-** |     **-** |  **299921.25 KB** |
| .NET Core 5.0 |  10000000 |                 List |  1,113.41 ms |   589.34 ms |   153.050 ms |         - |     - |     - |  299925.04 KB |
| **.NET Core 3.1** |  **10000000** |           **SortedList** | **10,358.00 ms** |   **354.21 ms** |    **91.988 ms** |         **-** |     **-** |     **-** |  **1371084.1 KB** |
| .NET Core 5.0 |  10000000 |           SortedList | 10,716.90 ms |   512.64 ms |   133.132 ms |         - |     - |     - | 1371084.54 KB |
| **.NET Core 3.1** |  **10000000** |                 **Void** |    **931.21 ms** |   **418.29 ms** |   **108.630 ms** |         **-** |     **-** |     **-** |   **65545.44 KB** |
| .NET Core 5.0 |  10000000 |                 Void |    856.34 ms |   753.24 ms |   195.613 ms |         - |     - |     - |   32777.39 KB |

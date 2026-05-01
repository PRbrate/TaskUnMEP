# TaskUnMEP

Projeto desenvolvido em **C# / .NET** - Desafio de seleção - Análise de registros de tempo

---

# Tecnologias Utilizadas

* .NET
* C#
* LINQ
* Docker
* Docker Compose
  
---

# Rodando via docker

1. Clone o repositório  
```
git clone https://github.com/PRbrate/TaskUnMEP.git
cd TaskUnMEP
```
2. Suba o container
``` docker compose up --build ```

# Estrutura do Projeto

```text
TaskUnMEP/
├── Program.cs
├── Models/
│   ├── UserTask.cs
│   ├── TaskData.cs
│   └── UserData.cs
├── data.json
├── README.md
├── Dockerfile
└── docker-compose.yml
```

---

# Explicação do Código

# Program.cs

Este é o arquivo principal da aplicação. Toda a lógica de leitura, processamento e geração do relatório está nele.

---

## 1. Leitura do Arquivo JSON

```csharp
string json = File.ReadAllText(path);
userTasks = JsonConvert.DeserializeObject<List<UserTask>>(json);
```

### Explicação

Lê todo conteúdo do arquivo JSON.

### `DeserializeObject`

Transforma o JSON em objetos C#.

---

## 2. Filtrar Registros Inválidos

```csharp
var usersValid = userTasks.Where(u => u.minutes >= 0).ToList();
```

### Explicação

Remove registros com minutos negativos.

---

## 3. Total de Minutos

```csharp
var totalMinutes = usersValid.Sum(u => u.minutes);
```
---

## 4. Agrupamento por Tarefa

```csharp
var taskByGroup = usersValid.GroupBy(u => u.taskId);
```

### Explicação

Agrupa todos os registros da mesma tarefa.

---

## 5. Criação do Relatório por Tarefa

```csharp
foreach(var group in taskByGroup)
```

### Explicação

Percorre cada grupo e calcula:

* total de minutos da tarefa
* nome da tarefa
* percentual sobre o total geral

---

### Soma da tarefa

```csharp
var totalByTask = group.Sum(v => v.minutes);
```

---

### Percentual

```csharp
double percentage =
(double)totalByTask / totalMinutes * 100;
```
---

### Objeto final

```csharp
new TaskData(id, name, sum, percentageReturn);
```

Cada tarefa vira um objeto estruturado.

---

## 6. Tarefa Mais Trabalhada

```csharp
var moreWork = taskToReturn.MaxBy(t => t.totalMinutes);
```

---

# Processamento por Usuário

---

## 7. Agrupar por Funcionário

```csharp
var employee = usersValid.GroupBy(u => u.userId);
```

### Explicação

Reúne todos os registros de cada colaborador.

---

## 8. Estatísticas por Usuário

Dentro do `foreach`:

```csharp
int totalEmpMinutes = emp.Sum(u => u.minutes);
```

Calcula total trabalhado pelo usuário.

---

### Tarefas distintas

```csharp
Distinct()
```

Remove repetições.

---

## 9. Top 3 Funcionários

```csharp
var top3Employees = employeeReturn
    .OrderByDescending(t => t.totalMinutes)
    .Take(3);
```

### Explicação

Mostra os 3 colaboradores com maior carga de trabalho.

---

## 10. Usuário com Mais Tarefas Diferentes

```csharp
var mostDistinctUserOnTasks =
employeeReturn
.OrderByDescending(e => e.GetTaskDist().Count())
.First();
```

### Explicação

Retorna quem participou da maior variedade de tarefas.

---

# Montagem do Resultado Final

---

## 11. Dictionary Final

```csharp
Dictionary<string, object> dataReturn =
new Dictionary<string, object>();
```

### Explicação

Estrutura dinâmica usada para gerar o JSON final.

---

## 12. Campos Inseridos

```csharp
dataReturn["totalMinutes"]
dataReturn["tasks"]
dataReturn["mostWorkedTask"]
dataReturn["top3TasksPercentage"]
dataReturn["top3Employees"]
dataReturn["mostDistinctUserOnTasks"]
dataReturn["ignoredRecords"]
```

### Explicação

Cada chave representa uma parte do relatório final.

---

## 13. Serialização Final

```csharp
string jsonReturn =
JsonConvert.SerializeObject(dataReturn, Formatting.Indented);
```

### Explicação

Transforma tudo em JSON formatado.

---

## 14. Escrita do Arquivo

```csharp
File.WriteAllText("/output/result.json", jsonReturn);
```

### Explicação

Cria o arquivo `result.json`. na pasta output

---

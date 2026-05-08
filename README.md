# 🏦 Banco Belo Horizonte

Sistema de gerenciamento de contas bancárias do **Banco Belo Horizonte**, desenvolvido em **C# / .NET 8**, aplicando os princípios de **Orientação a Objetos** (herança, abstração, encapsulamento e polimorfismo).

---

## 📋 Sobre o Projeto

O **Banco Belo Horizonte** é uma aplicação de console interativa que simula operações bancárias reais, como criação de contas, depósitos, saques e transferências. O projeto foi desenvolvido como parte do aprendizado de C# e serve de base para estudo.

---

## 🗂️ Estrutura de Classes

```
ContaBancaria/
├── Menu.cs               # Ponto de entrada — menu interativo (Main)
├── Cores.cs              # Utilitário de cores ANSI para o terminal
├── Conta.cs              # Classe abstrata base
├── ContaCorrente.cs      # Herda Conta — adiciona Limite de crédito
├── ContaPoupanca.cs      # Herda Conta — adiciona Aniversário da poupança
├── ContaRepository.cs    # Interface com os métodos do sistema
└── ContaController.cs    # Implementa ContaRepository
```

### Diagrama de Classes

```
          ┌─────────────────────┐
          │    <<interface>>    │
          │  ContaRepository    │
          └─────────┬───────────┘
                    │ implements
          ┌─────────▼───────────┐
          │  ContaController    │
          └─────────────────────┘

          ┌─────────────────────┐
          │   <<abstract>>      │
          │       Conta         │
          └──────────┬──────────┘
                     │ extends
          ┌──────────┴──────────┐
          │                     │
 ┌────────▼────────┐   ┌────────▼────────┐
 │  ContaCorrente  │   │  ContaPoupanca  │
 └─────────────────┘   └─────────────────┘
```

---

## ⚙️ Funcionalidades

| # | Funcionalidade | Descrição |
|---|---|---|
| 1 | Criar conta | Cria Conta Corrente (com limite) ou Conta Poupança (com aniversário) |
| 2 | Listar contas | Exibe todas as contas cadastradas |
| 3 | Buscar conta | Localiza uma conta pelo número |
| 4 | Atualizar titular | Altera o nome do titular da conta |
| 5 | Deletar conta | Remove uma conta do sistema |
| 6 | Depositar | Adiciona saldo a uma conta |
| 7 | Sacar | Retira saldo (Conta Corrente usa limite de crédito) |
| 8 | Transferir | Move valor entre duas contas |

---

## 🧱 Detalhes das Classes

### `Conta` (abstrata)
Classe base com os atributos e métodos comuns a todos os tipos de conta.

| Atributo | Tipo | Descrição |
|---|---|---|
| `Numero` | `int` | Gerado automaticamente de forma sequencial |
| `Agencia` | `int` | Número da agência |
| `Tipo` | `int` | `1` = Corrente / `2` = Poupança |
| `Titular` | `string` | Nome do titular |
| `Saldo` | `decimal` | Saldo atual |

**Métodos:** `Depositar()`, `Sacar()`, `Visualizar()` *(abstrato)*

---

### `ContaCorrente` → herda `Conta`

| Atributo extra | Tipo | Descrição |
|---|---|---|
| `Limite` | `decimal` | Limite de crédito disponível |

- Sobrescreve `Sacar()` para permitir saques até `Saldo + Limite`.

---

### `ContaPoupanca` → herda `Conta`

| Atributo extra | Tipo | Descrição |
|---|---|---|
| `AniversarioPoupanca` | `int` | Dia do mês de aniversário (1–28) |

---

### `ContaRepository` (interface)
Define o contrato de operações:

```csharp
void CriarConta();
void ListarTodas();
void BuscarPorNumero(int numero);
void AtualizarConta(int numero);
void DeletarConta(int numero);
void Depositar(int numero, decimal valor);
void Sacar(int numero, decimal valor);
void Transferir(int origem, int destino, decimal valor);
```

---

### `ContaController`
Implementa `ContaRepository` e gerencia a lista de contas em memória (`List<Conta>`).

---

### `Cores`
Utilitário estático com constantes ANSI e métodos de exibição colorida:

```csharp
Cores.Sucesso("Operação realizada!");  // verde
Cores.Erro("Saldo insuficiente.");     // vermelho
Cores.Aviso("Atenção!");               // amarelo
Cores.Info("Conta nº 1");             // ciano
Cores.Titulo("Menu Principal");        // azul
```

---

## ✅ Validações implementadas

- Saldo insuficiente bloqueado antes do saque
- Conta Corrente permite saque até o limite de crédito
- Verificação de existência da conta antes de qualquer operação
- Validação de entradas inválidas (valor negativo, texto no lugar de número)
- Consistência garantida nas transferências (débito só ocorre se o saque for aprovado)

---

## 🚀 Como executar

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Passos

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/conta-bancaria.git

# Entre na pasta do projeto
cd conta-bancaria/ContaBancaria

# Execute
dotnet run
```

---

## 🖥️ Exemplo de uso

```
====================================================
                Banco Belo Horizonte
====================================================

  1 » Criar conta
  2 » Listar todas as contas
  3 » Buscar conta por número
  4 » Atualizar titular
  5 » Deletar conta
  6 » Depositar
  7 » Sacar
  8 » Transferir
  0 » Sair

Escolha uma opção: 1

Tipo (1 = Corrente / 2 = Poupança): 1
Agência: 123
Titular: Gabriel Silva
Saldo inicial: R$ 500
Limite de crédito: R$ 1000

✔ Conta nº 1 criada com sucesso!
```

---

## 🧠 Conceitos de POO aplicados

| Conceito | Aplicação |
|---|---|
| **Abstração** | Classe `Conta` define o molde genérico |
| **Herança** | `ContaCorrente` e `ContaPoupanca` estendem `Conta` |
| **Polimorfismo** | `Visualizar()` e `Sacar()` têm comportamentos distintos por tipo |
| **Encapsulamento** | Atributos com modificadores `private`/`protected` e acesso via propriedades |
| **Interface** | `ContaRepository` define o contrato de operações |

---

## 📌 Possíveis melhorias futuras

- [ ] Persistência em banco de dados (Entity Framework Core + SQL Server)
- [ ] Histórico de transações por conta
- [ ] Cálculo automático de juros na Conta Poupança

---

## 👤 Autor

Desenvolvido por **Gabriel Soares Reis** como parte do **Projeto AceleraMaker 2026**.

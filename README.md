# Sistema de Gerenciamento Hoteleiro

Aplicação desktop desenvolvida para auxiliar no gerenciamento das principais atividades de um hotel. O sistema reúne cadastros, reservas, controle administrativo e atendimento em uma interface feita com Windows Forms.

## Funcionalidades

- Login e gerenciamento de usuários
- Cadastro de clientes
- Cadastro de funcionários
- Gerenciamento de reservas e quartos
- Registro de consumo e serviços de quarto
- Controle de estoque
- Cadastro de fornecedores
- Controle de achados e perdidos
- Atendimento ao cliente (SAC)
- Relatórios e informações financeiras
- Diferentes níveis de acesso para usuários

## Tecnologias utilizadas

- C#
- Windows Forms
- .NET Framework 4.7.2
- SQLite
- ADO.NET
- Visual Studio

## Requisitos

Para executar ou modificar o projeto, é necessário:

- Windows 10 ou superior
- Visual Studio 2019 ou superior
- .NET Framework 4.7.2
- Carga de trabalho “Desenvolvimento para desktop com .NET”

## Como executar

1. Clone este repositório:

```bash
git clone URL_DO_SEU_REPOSITORIO
```

2. Abra o arquivo `WindowsFormsApp1.csproj` no Visual Studio.

3. Aguarde o carregamento das dependências.

4. Selecione a configuração `Debug` ou `Release`.

5. Pressione `F5` ou clique em **Iniciar**.

O banco de dados SQLite é criado automaticamente na pasta `Data` durante a primeira execução.

## Acesso para demonstração

```text
Usuário: admin
Senha: admin
```

> As credenciais padrão são destinadas apenas à demonstração. Recomenda-se alterar a senha ao utilizar o sistema em outro ambiente.

## Banco de dados

O sistema utiliza um banco SQLite local chamado `hotel.db`. Ele armazena informações como:

- Usuários
- Clientes
- Funcionários
- Quartos
- Reservas
- Produtos e estoque
- Consumos
- Fornecedores
- Chamados do SAC
- Achados e perdidos

## Arquitetura

O projeto está organizado em três áreas principais:

- **Domain:** modelos e entidades do sistema.
- **Infrastructure:** persistência e acesso ao banco SQLite.
- **Presentation:** formulários, componentes visuais e interação com o usuário.


## Licença

Este projeto foi desenvolvido para fins acadêmicos.

# AgendaTenis.Jogadores


## Sobre<a name = "sobre"></a>
AgendaTenis.Jogadores é um microsserviço da aplicação AgendaTenis cujo objetivo é registrar os dados dos jogadores de tênis cadastrados.
Este serviço é constituído por uma Web Api e um Worker Service escritos em .NET 8 e utiliza o Postgresql para persistência de dados (mais detalhes na seção *Descrição Técnica*)

## Endpoints<a name = "endpoints"></a>

### Completar perfil
Quando o usuário cria uma conta no sistema, ele fornece apenas as credenciais básicas (e-mail e senha).\
Para ter um perfil de tenista completo no sistema, o usuário deverá utilizar esta feature e informar os dados adicionais a seguir:
- Nome
- Sobrenome 
- Data de nascimento
- Telefone
- País
- Estado
- Região
- Cidade
- Mão dominante
- Backhand
- Estilo de jogo

Com isso, o usuário terá um perfil completo que poderá ser encontrado por outros jogadores interessados em jogar com ele.

**Rota**: Api/Jogadores/Perfil/Completar\
**Método HTTP**: POST\
**Autenticação**: Necessita token jwt gerado em Api/Identity/GerarToken, do contrário retorna status 401 (Unauthorized)\
**Autorização**: Não tem políticas de autorização, somente autenticação é suficiente

### Buscar adversários
Essa feature é muito útil para o tênista encontrar adversários cadastrados na plataforma.\
É possível encontrar outros tenistas filtrando por região e categoria.

**Rota**: Api/Jogadores/Adversarios/Buscar?pais=Brasil&estado=S%C3%A3o%20Paulo&cidade=Campinas&categoria=2\
**Método HTTP**: GET\
**Autenticação**: Necessita token jwt gerado em Api/Identity/GerarToken, do contrário retorna status 401 (Unauthorized)\
**Autorização**: Não tem políticas de autorização, somente autenticação é suficiente\
**Observações**: Se necessário utilize a seção [Valores de domínio](#valores_dominio) para encontrar os códigos para **Categoria**, **ModeloPartida**, **StatusConvite** e **StatusPlacar**

### Obter resumo
O usuário logado pode acessar este endpoint para obter seu resumo de tênista.\
Com isso ele irá obter as seguintes informações:
- Id
- Nome Completo
- Idade
- Pontuação
- Categoria

**Rota**: Api/Jogadores/Resumo\
**Método HTTP**: GET\
**Autenticação**: Necessita token jwt gerado em Api/Identity/GerarToken, do contrário retorna status 401 (Unauthorized)\
**Autorização**: Não tem políticas de autorização, somente autenticação é suficiente\
**Observações**:\
Se necessário utilize a seção [Valores de domínio](#valores_dominio) para encontrar os códigos para **Categoria**, **ModeloPartida**, **StatusConvite** e **StatusPlacar**\
Contém cache com 2 minutos de expiração.

## Valores de domínio <a name = "valores_dominio">
Valores numéricos de domínio (enums) são utilizado em diversos locais da aplicação, tais como parâmetros de query (ie., feature Buscar Jogadores), em requests http (ie., feature Responder Convite) e responses da api (ie., feature obter resumo do tenista).\
Segue abaixo a lista de valores de domínio:
- Categoria
    - Atp = 1
    - Avançado = 2
    - Intermediário = 3
    - Iniciante = 4 
 
## Descrição técnica<a name = "descricao_tecnica"></a>
Segue a descrição técnica do AgendaTenis.Partidas.

- Projeto: AgendaTenis.Core.Jogadores
- Modelo de dados:
    - Jogador
        - Id: int
        - UsuarioId: int
        - Nome: string
        - Sobrenome: string
        - DataNascimento: DateTime
        - Telefone: string
        - Pais: string
        - Estado: string
        - Cidade: string
        - MaoDominante: string
        - Backhand: string
        - EstiloDeJogo: string
        - PontuacaoId: Guid
        - DataCriacao: DateTime
        - PontuacaoAtual: double
- Banco de Dados: Postgresql
- Cache: Redis
- Mensageria: RabbitMQ
- Acesso a dados: O acesso a dados foi abstraído com uso do EntityFramework.Core
- Observações:
    - Ao contrário do contexto identity, aqui eu **não** utilizei o "Repository Pattern". Dessa forma, estou injetando o DbContext diretamente nos fluxos. O motivo desta decisão foi testar um abordagem diferente do repository pattern.
    - **Não** Utilizei o FluentValidation para realizar validações de dados, criei validações simples utilizando POCO (Plain Old CLR Object)
- Dependências:
    - AgendaTenis.Notificacoes.Core (pacote nuget) versão 1.0.1
    - AgendaTenis.Cache.Core (pacote nuget) versão 1.0.1
    - AgendaTenis.Eventos.Core (pacote nuget) versão 1.0.0
    - Microsoft.EntityFrameworkCore.SqlServer versão 8.0.4
    - Microsoft.AspNetCore.Authentication.JwtBearer versão 8.0.6
    - Microsoft.EntityFrameworkCore.Tools versão 8.0.6
    - Microsoft.VisualStudio.Azure.Containers.Tools.Targets versão 1.20.1
    - Swashbuckle.AspNetCore versão 6.4.0
    - Microsoft.Extensions.Hosting 8.0.0
    - Microsoft.VisualStudio.Azure.Containers.Tools.Targets 1.20.1
 
### Docker
- Este repositório possui 2 executáveis (AgendaTenis.Jogadores.WebApi e AgendaTenis.Jogadores.WorkerService), por isso criei 2 Dockerfiles:
  - O Dockerfile de AgendaTenis.Jogadores.WebApi chama-se DockerfileApi
  - O Dockerfile de AgendaTenis.Jogadores.WorkerService chama-se DockerfileWorkerService
- Utilize as instruções presentes na seção *Como executar* do repositório [Agte](https://github.com/AgendaTenisOrg/AgendaTenis.WebApp) para executar a stack inteira da aplicação

Observação: Se eu tivesse separado os projetos AgendaTenis.Jogadores.WebApi e AgendaTenis.Jogadores.WorkerService em repositórios exclusivos, teria sido necessário criar um terceiro repositório para o AgendaTenis.Jogadores.Core (que contém a lógica do Core do contexto de Jogadores) e publicar um pacote Nuget para ser reaproveitável entre AgendaTenis.Jogadores.WebApi e AgendaTenis.Jogadores.WorkerService. Isso me pareceu muito trabalho para pouco ganho, então mantive ambos os executáveis neste repositório (por isso temos 2 Dockerfiles).

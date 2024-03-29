# Portfolio

## Introduction

I have created this portfolio to help provide future colleagues an example of my programming abilities
as a .net developer. I should take this moment to highlight that this is not a finished product and there
are some areas that I would not consider production ready.

I decided to create a product catalogue for a sweats company which is a .net 6 lightweight API. I have also
included a console application, that can be used to publish new products onto a queue, the idea is that a
consumer of these products can process these outside of the main API thread.

## Why

* I've have 10+ years experience building applications, some monolithic and others distributed microservices:
  * API's
  * Multi-tenant full stack .net applications
  * Console applications
  * Azure Serverless functions
  * ETL pipelines using SSDT, serverless functions
  * API integrations
  * low/no code: Logic-Apps
  * publishers/consumers working with queues
  * continuous integration and delivery using many different platforms: github, azure devops, appveyor
  * writing unit/integration/smoke tests and integrating these into the software delivery lifecycle.
* I wanted to build something that is lightweight in effort, that provides you with a good example of my abilities.
I thought this fictitious application would be a good place to start, to allow you to asses my coding style and approach.

## Highlights

* I have created a [SQL schema](/Database/Models/), using entity framework core, code first
* I have used the [specification software pattern](/Database/SpecificationPattern/) to [search](/Database/Search/DatabaseSearchOrchestrator.cs) for products
* Unit-testing:
  * Use Moq.AutoMock to instantiate unit under test: saves time as there is no need to create mocks, and if a constructor call signature change there is no additional work to fix broken tests
  * Use  [Nunit.Framework.ValuesAttribute](/Database.Tests/Enums/MapEnumToEnum/MapProductToProductType/EnsureAllProductsAreMapped.Tests.cs#L11) to allow a test to scale with the Enum we are testing - protection if a new enum value is added and the engineer doesn't see the need to extend mapping logic.
* [Middleware](API/Middleware/ExceptionHandlerMiddleware.cs): Exception handling to capture any exceptions that bubble up to the route of the API
* Github pipelines for Continuous integration / deployment
* IaC: [Terraform files](/IaC/) to create cloud resources for this project
* CICD: [GitHub action files](/.github/workflows/) to manage continuous integration, creation of resources in azure (on-demand) & deployment of artifacts, run & persist postman/plawright integration tests, and the final step  to teardown azure resources.
  * [FIle](.github/workflows/main.pr.yml) to orchestrate actions when a pull-request is submitted.
  * [FIle](.github/workflows/main.yml) to orchestrate actions when a branch is merged into main.
* Postman Requests with tests checking:
  * status code
  * Content-type is JSON
  * Schema
  * Search functionality
* Plawright e2e tests
  * This is my first time using this library. At present the coverage is low, but we are testing the following:
    * [main products page](UI/tests/products.initial.load.count.test.ts)
    * [search functionality](UI/tests/search/)
    * [creation of a product](UI/tests/create/create.product.test.ts)
* SvelteKit UI
  * I have been keeping an eye on SvelteKit for a while and I wanted to try out the framework. I'm not a frontend expert, but I know enough to dabble.  

## Honest appraisal

* Specification pattern:
  * Can this be extended to be able to use EntityFrameworkQueryableExtensions.ThenInclude method: I need to spend some extra time investigating this further.
* Overall the unit-test coverage should be better.
* Infrastructure:
  * We have setup the server with simple username & password access. I would consider turning off this feature and only allowing access to the database by a managed identity (azure AD). If this security hardening was setup we would not have any username and passwords in our application configuration.
  * If we wanted to create different environments from this source code, some of the terraform runtime variables would need to be reviewed.
* Observability should be better: need to create more logs for analysis and install application insights
* At present its only myself working on this repository, at some point a branching strategy should be adopted - for example: GitFlow
* UI: The current effort is good enough for an MVP, the design is not going to win any awards (review the screen-grabs below to view its current design) but the base of the product has good foundations to be built upon. We are using tailwind css and SvelteKit of which I've very much enjoyed using:
  * We can search for existing products, and create/archive.
    * The search is performed on the backend, should we be enhancing this functionality to include:
      * Add some ordering functionality
      * And filtering.
  * The design is responsive, with a Hamburger for the navigation
  * We are running a small collection of playwright tests post deployment - these tests should be expanded, and we should run these using different browsers.
  * I need to agree on, or go searching for an industry standard on component/variable naming standards and enforce these.
* Playwright
  * The coverage of these tests should be extended.
  * I would be tempted to rewrite these in c# - to compare the experience, as I'm wandering if there is any advantage/disadvantages in doing so?

## Instructions

### For setting up your local database

After downloading the repository, please run the following commands to create
the database on your local machine

```visual studio
update-database
```

### Publisher

#### On your local environment the publisher requires the azurite emulator to be running

When running the publisher locally, you will need to startup the azurite emulator,
follow these steps:

Create a folder on c drive to host the persisted data that azurite will create

```powershell
New-Item C:\azurite
```

pull azurite docker image

```powershell
docker pull mcr.microsoft.com/azure-storage/azurite
```

check image exists

```powershell
docker image ls
```

run azurite from docker image

```powershell
docker run -p 10000:10000 -p 10001:10001 -p 10002:10002 -v c:/azurite:/data mcr.microsoft.com/azure-storage/azurite
```

### Infrastructure as Code

We are using terraform to create the required infrastructure in Azure. To execute locally, you will need:

* install terraform: <https://learn.hashicorp.com/tutorials/terraform/install-cli?in=terraform/azure-get-started>

* install Azure CLI: <https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli>

* login to azure via the Azure CLI

```powershell
az login
```

* We need to create a terraform tfvars file - to replace sensitive variables at execution time: name the file terraform.tfvars, and add to this the following variables:

```terraform
sql_instance_administrator_login_username = "place a suitable username here"
sql_instance_administrator_login_password = "place a strong password here"
sql_instance_name = "name the instance"
environment_prefix = "environment"
```

* now we need to run the following terraform commands to create the resources in azure

```terraform
terraform init
terraform plan -out main.tfplan
terraform apply main.tfplan
```

* when we are done we should clean up the resources

```terraform
terraform destroy -auto-approve
```

### SvelteKit UI

```powershell
npm install
npm run dev
```

We are using the Tailwind CSS framework.

* Full screen
  * [main products UI](UI.screen.grabs/full.screen.main.products.page.png)
  * [product editing modal](UI.screen.grabs/full.screen.edit.modal.png)
  * [product archive modal](UI.screen.grabs/full.screen.archive.modal.png)
* Responsive
  * [main products UI](UI.screen.grabs/responsive.screen.main.png)
  * [product editing modal](UI.screen.grabs/responsive.screen.edit.png)
  * [Hamburger/navigation](UI.screen.grabs/responsive.screen.hamburger.png)

#### UI End-to-end tests using Playwright

Run the following command from the route of the UI folder

```powershell
npx playwright test
```

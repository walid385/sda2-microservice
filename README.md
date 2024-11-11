# Microservice-Based Point of Sale (PoS) System Documentation

## Project Overview

This project uses a microservices architecture for a Point of Sale (PoS) system. Designed on SQLite and [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) 7, every microservice runs autonomously with its own database and is committed to a certain business domain. The system uses RabbitMQ for inter-service communication, therefore enabling asynchronous, event-driven interactions. For synchronous communication, the project makes use of .NET's built-in HttpClient, which ensures quick and reliable communication between services.

The initiative also combines Ocelot as an API Gateway to simplify request routing across services and Docker for containerization, therefore enabling simple deployment and testing. Designed to run both locally and in the cloud, a Function-as- a- Service (FaaS) component installed as an Azure Function offers scalable serverless capability inside the system.

## Project Setup Instructions

This guide will walk you through setting up the entire application, including Docker, RabbitMQ, and testing the Ocelot API Gateway. Follow the steps carefully to get the project up and running.

### Prerequisites

Before running the project, make sure you have the following software installed:

1. **Docker**

   - Docker is required to containerize and run the microservices and other components.
   - You can install Docker by visiting [Docker Install Guide](https://docs.docker.com/get-docker/).
2. **Docker Compose**

   - Docker Compose is required to run the multiple containers needed for this project.
   - Docker Compose is typically included with Docker Desktop, but you can also refer to [Docker Compose Install Guide](https://docs.docker.com/compose/install/) for specific instructions.
3. **Azure Functions Core Tools**

   - Azure Functions Core Tools are required to run the Azure Function locally. Install Azure Functions Core Tools by following the instructions at [Azure Functions Core Tools Installation Guide](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=macos%2Cisolated-process%2Cnode-v4%2Cpython-v2%2Chttp-trigger%2Ccontainer-apps&pivots=programming-language-csharp#install-the-azure-functions-core-tools).
4. **.NET Runtime (Optional)**

   - You do **not need to install the full .NET SDK** unless you want to modify the code. The runtime is included in the Docker images.
   - If desired, you can install the .NET SDK by visiting [Microsoft .NET Install Guide](https://dotnet.microsoft.com/download).

### Project Setup Steps

### 1. Unzip the Project

- First, unzip the project file that you received.
- Navigate to the unzipped directory containing the project files.

### 2. Open a Terminal

- Open a terminal (or PowerShell if on Windows).
- Navigate to the root directory of the unzipped project:
  ```sh
  cd path/to/unzipped/project
  ```

### 3. Running the Application with Docker Compose

- The entire project is set up to run using **Docker Compose**.
- Depending on your Docker version, you may need to use one of the following commands to start the application:

  - **For older versions**:
    ```sh
    docker-compose up --build
    ```
  - **For newer versions**:
    ```sh
    docker compose up --build
    ```
  - If the `--build` flag does not work, try running:
    ```sh
    docker compose up
    ```
- This command will build and run all necessary containers, including microservices, the Ocelot API Gateway, and RabbitMQ.

### 4. RabbitMQ Setup

- RabbitMQ is included as a Docker container within the project, so **you do not need to install RabbitMQ manually**.
- You can access the RabbitMQ management dashboard at [http://localhost:15672](http://localhost:15672) once the containers are running.
- Default credentials for RabbitMQ:
  - **Username**: `guest`
  - **Password**: `guest`

### 5. Ocelot API Gateway Setup

- **Ocelot** is used as the API Gateway for this project.
- **You do not need to install Ocelot separately**—it is already included in the Docker image for the gateway.
- The API Gateway is accessible at [http://localhost:7777](http://localhost:7777).

### 6. Running the Azure Function Locally

1. Navigate to the Azure Function directory:

   - `cd path/to/unzipped/project/faas/LowStockFunctionApp`
2. Start the Function:

- Run the function using Azure Functions Core Tools:

  - `func start`
- This will start the function locally, where it will listen for events from RabbitMQ.

### 7. Testing the Endpoints

- Once the services are running, you can test the endpoints using tools like **Postman** or **curl**. Alternatively, you can use Swagger with the specific Service Ports [mentioned in Microservices Detail](#Microservice-Details).
- The base URL for all API requests is: [http://localhost:7777](http://localhost:7777), endpoints [mentioned in Microservices Detail](#Microservice-Details)

## Stopping the Application

- To stop the running containers, use the following command in the terminal:
  - **For older versions**:
    ```sh
    docker-compose down
    ```
  - **For newer versions**:
    ```sh
    docker compose down
    ```
- This will stop and remove all running containers.

### Additional Notes

- **Port Conflicts**: Make sure that the ports defined in `docker-compose.yml` (e.g., `5000`, `5001`, `7777`, `15672`) are not in use by other applications.
- **Docker Compose File**: The `docker-compose.yml` file includes definitions for all microservices, RabbitMQ, and the API Gateway.
- **Logs**: You can view logs for troubleshooting by running:
  - **For older versions**:
    ```sh
    docker-compose logs
    ```
  - **For newer versions**:
    ```sh
    docker compose logs
    ```

- ## Troubleshooting

- If `docker compose up` fails, ensure you have the latest version of Docker installed.
- If you encounter issues with the `--build` flag, try removing it and running `docker compose up` again.

- **Service Not Responding**: Ensure Docker containers are running. Use `docker ps` to verify.
- **RabbitMQ Issues**: Ensure RabbitMQ is running and that services are connected correctly.
- **Function Errors**: If the Azure Function fails locally, check the `RabbitMQConnection` setting in `local.settings.json`.
- **WSL Error for Windows User**: If you get this error:
![Docker WSL Error](/README_Files/DockerWSLError.png)
Make sure Virtualisation is enabled (In Taskmanager):
![Task Manager screenshot](/README_Files/VirtualizationTaskmanager.png)
Check out for the BIOS-Configuration: https://www.youtube.com/watch?v=LQIyowZMiY8

## Summary Setup

- **Install Docker and Docker Compose**.
- **Unzip the project** and **navigate to the directory**.
- **Run `docker-compose up --build` or `docker compose up`** to start all services.
- **Access RabbitMQ** at [http://localhost:15672](http://localhost:15672).
- Start the Azure Function by navigating to its folder and running `func start`.
- **Test endpoints** via [http://localhost:7777](http://localhost:7777).
- **Stop services** using `docker-compose down` or `docker compose down`.

## Key Components

The PoS system includes the following core services:

1. **CustomerService** - Manages customer information, rewards, and cart operations.
2. **InventoryService** - Manages product inventory, stock levels, and low-stock alerts.
3. **SalesService** - Handles sales transactions, ticket issuance, and customer rewards updates.
4. **OrderManagementService** - Manages vendor orders and restocking processes.
5. **VendorService** - Stores vendor information and provides vendor details for orders.
6. **EmployeeService** - Manages employee data, permissions, and store assignments.
7. **TaxService** - Calculates and updates tax rates as required.

## Microservice Architecture

### Why Choose Microservices?

1. **Scalability**: Each service can be scaled independently, allowing resource allocation based on demand.
2. **Fault Isolation**: Service failures are contained, reducing the risk of cascading failures.
3. **Technology Independence**: Different teams can select the best technology stack for each service.
4. **Modularity**: Each service can be developed, deployed, and maintained independently.

**Trade-offs**:

- **Increased Complexity**: Microservices require robust inter-service communication, orchestration, and monitoring.
- **Data Consistency Challenges**: Ensuring data consistency across services is complex, often requiring event-driven patterns.
- **Higher Operational Overhead**: Multiple services mean more resources are needed for deployment, monitoring, and troubleshooting.

### Biggest challenges if we keep the database together

- **Performance Bottlenecks**: A single shared database would need to handle the load of all services, leading to resource contention. For example, simultaneous queries from InventoryService, OrderManagementService, and SalesService could cause performance degradation.
- **Data Coupling and Development Constraints**: A single database creates tight coupling between services, making schema changes difficult. Modifying a table to support a new feature in one service could inadvertently break another service’s functionality.
- **Scalability Limitations**: With a single database, scaling individual services independently becomes challenging. For instance, if SalesService experiences high demand, it cannot be scaled separately without impacting the shared database’s performance, reducing overall system flexibility and hindering the ability to optimize resources for specific services.

## Data Decomposition

### Focus

The data in this Point of Sale (PoS) system has been decomposed according to microservice architecture principles, allowing each service to govern its own domain-specific data in alignment with its primary business functions. This structure enhances the system's scalability, modularity, and resilience by enabling services to evolve independently. A central focus of this decomposition is on the **customer journey**, ensuring that services like CustomerService are designed to satisfy most of the customer’s needs autonomously.

### Why Divide Data by Service?

- **Enhanced Customer Experience**: Each service predominantly manages essential customer journey activities—such as browsing, cart management, and returns—within Customer Service, therefore decreasing reliance on other services. This configuration enables Customer Service to autonomously provide a seamless user experience, therefore replicating the modular workflows observed in real-world applications, despite the fact that certain dependencies, such as order verification, remain separate.
- **Service Autonomy**: Individual services prevent direct data interactions between them and support isolated schema changes and updates by operating on distinct databases. This autonomy guarantees that customers will encounter a scalable, streamlined interface that is free from interference from backend dependencies.
- **Minimal Cross-Service Coupling**: Each service has exclusive control over its own data, thereby reducing inter-service dependencies. For instance, modifications to inventory data do not affect customer profiles, and modifications to order details do not disrupt customer rewards. This isolation fosters adaptability throughout the system.
- **Scalability and Resource Optimization**: The decomposition of data enables each service to scale in accordance with its demand. For example, SalesService can manage high transaction volumes without imposing a burden on CustomerService's database, thereby guaranteeing that resources are optimized to meet the specific needs of each service.
- **Enhanced Security and Access Control**: Access control is more precise by limiting data within each service, thereby protecting sensitive information related to consumers, orders, and transactions.
- **Fault Isolation**: The enhancement of fault tolerance is achieved by isolating data by service. This ensures that an issue in one service's database remains isolated and does not affect the entire system.

## Architecture Overview

The architecture was chosen to benefit from the flexibility, scalability, and fault tolerance that microservices provide over monolithic solutions. The system can be maintained, updated, and scaled separately by breaking the application down into multiple services, each dedicated to a specific business domain (for example, CustomerService, InventoryService, SalesService). This independence is critical for administering complex applications, especially when demand varies across multiple portions of the system.

![Microservice Architecture](/README_Files/Microservice_Archtecture.png)

#### Key Reasons to Choose Microservices:

1. **Scalability**: Each microservice can be scaled according to demand. For example, if 'SalesService' sees heavy traffic, it can be scaled separately without affecting other services. This strategy maximizes resource consumption and enables targeted growth.
2. **Development Agility**: Multiple development teams can collaborate on individual services without interfering with one another. This modularity shortens development, testing, and deployment cycles, making it easier to release updates or new features.
3. **Fault Isolation**: Issues in one service do not spread to others. If 'InventoryService' finds a problem, the failure is limited to that service, protecting the entire system functionality and decreasing user downtime.
4. **Focused Security**: Each service only handles the data it requires, allowing for tighter security controls and less exposure of sensitive information. For example, only 'CustomerService' handles customer data, and 'OrderManagementService' solely manages order data.

#### API Gateway using Ocelot

Ocelot serves as the API gateway, centralizing client requests and routing them to the appropriate microservice. The API gateway provides various benefits:

- **Unified Entry Point**: It makes interactions easier for clients by offering a single access point rather than several service endpoints.
- **Load Balancing and Caching**: Ocelot can balance loads across services, resulting in improved performance and lower latency.
- **Request Aggregation**: The gateway can aggregate requests, combine responses from many services as needed, and optimize data delivery to customers.

#### Asynchronous Communication With RabbitMQ

RabbitMQ is used as a message bus to provide asynchronous, event-driven communication among services. Here's why we chose RabbitMQ:

1. **Event-Driven Model**: RabbitMQ makes the system event-driven, allowing services to respond to events published by others without direct dependencies. For example, 'InventoryService' can generate a 'LowStockEvent' that 'VendorService' listens for to initiate restocking. This loosely linked system increases flexibility and robustness.
2. **Enhanced Resilience and Responsiveness**: RabbitMQ allows services to function independently and continue processing requests even while other services are unavailable. For example, 'OrderManagementService' can queue messages in RabbitMQ and handle them as needed, eliminating system bottlenecks.
3. **Decoupling Services**: RabbitMQ reduces the interdependence between services, making them more versatile and easier to maintain. Services do not need to know each other's implementations; all they need to know is the message structure. This enables each service to evolve independently.
4. **Reliability**: RabbitMQ supports message persistence, which ensures that no messages are lost even if a service is briefly unavailable. This dependability is vital for delivering a consistent client experience, where event-based actions (such as stock updates) are required.

In summary, this microservices architecture, which includes Ocelot and RabbitMQ, provides a solid, scalable basis. It enables services to scale, adapt, and remain independent while offering a seamless customer experience via event-driven interactions and centralized request management.

## Detailed Database Analysis

The database structure for each service has been carefully aligned with both microservice principles and customer journey needs, with each table tailored to specific service functionalities.

| Service                          | Database Tables                                                                        | Description                                                                                                                                                                                                                                                                                                                                                                                                                   |
| -------------------------------- | -------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **CustomerService**        | `customer_info`, `cart_inprogress`, `item_list`, `gift_card`, `return_table` | **Customer Journey Focus**: This service is pivotal in the customer journey, handling customer profiles, carts, returns, and rewards. By centralizing core activities such as viewing items, managing carts, and processing returns, CustomerService provides an independent and seamless user experience. Dependencies, like order status checks, are minimal and integrated smoothly to avoid disrupting the journey. |
| **InventoryService**       | `product_inventory`                                                                  | **Inventory Management**: Manages product stock levels and low-stock alerts. InventoryService operates independently, ensuring accurate stock updates and automated low-stock notifications, supporting restocking without impacting customer-facing services.                                                                                                                                                          |
| **SalesService**           | `ticket_system`, `registers_table`                                                 | **Transaction Processing**: Manages sales ticketing, billing, and payment processing. SalesService handles transaction-heavy periods independently by isolating ticket and register data, allowing customers to check out smoothly even during peak shopping seasons.                                                                                                                                                   |
| **OrderManagementService** | `orders`, `orders_ticket`, `cart`                                                | **Order and Restocking Management**: Responsible for inventory orders and restocking workflows. By isolating order data, OrderManagementService can optimize order processes without affecting other services. This setup enables customers to place and track orders reliably within CustomerService’s interface, while OrderManagementService manages backend order lifecycle operations.                            |
| **VendorService**          | `vendor_info`                                                                        | **Vendor Data Management**: Stores essential vendor information for inventory fulfillment. The isolated setup allows VendorService to handle supplier relationships and updates without impacting the ordering workflows of CustomerService or InventoryService.                                                                                                                                                        |
| **EmployeeService**        | `employee_info`, `stores`                                                          | **Employee and Store Data**: Manages employee records, roles, permissions, and store assignments. By isolating this data, EmployeeService ensures secure, role-based access control while supporting broader store operations.                                                                                                                                                                                          |
| **TaxService**             | `tax_table`                                                                          | **Tax Rate Management**: Calculates and applies tax rates to sales transactions. By isolating tax data, TaxService enables independent tax rate updates essential for compliance, without disrupting customer or sales workflows.                                                                                                                                                                                       |

### Reasons for implemented decomposition

With Customer Service enabling consumers to manage basic activities independently, like browsing, cart management, and incentives, the decomposition gives an effective and smooth customer journey top priority. Simple integration of dependencies like order verification or inventory checks helps to preserve a coherent experience.

This division of the database served to accomplish:

1. Every service runs autonomously, keeping its own data under control free from reliance on another. This facilitates change or update making without affecting the whole system.
2. Every service may be scaled separately, which lets the system effectively manage higher loads by just increasing the sections that demand it. For example, the Sales Service can be expanded without compromising other services during periods of maximum buying.
3. Having each service oversee its own database helps to reduce the difficulty of handling several relationships in a single monolithic database in development and maintenance. This lets one simplify maintenance and enable targeted development.
4. Problems in one service, including data corruption or heavy load, are isolated from other services via **fault isolation**. Should the Inventory Service fail, for instance, the Customer Service will not suffer, therefore guaranteeing a better customer experience.
5. Sensitive data is accessible and handled just by the services that demand it since every service manages its own data. This lessens security threats and restricts data exposure.
6. The decomposition guarantees that Customer Service can manage most client interactions without delays resulting from dependencies, therefore supporting an effective customer journey. Backend integration of services including inventory and order administration guarantees the consumer a flawless experience.

## Communication Flow

The services communicate asynchronously through RabbitMQ and synchrounosly through .NET's built-in HttpClient. RabbitMQ allows for decoupled interactions, especially when one service needs to respond to events generated by another service.

Our group aimed to enhance the core application by building upon the basic CRUD operations in some critical areas.

### Service Interactions

#### Synchrounous Communications:

1. **Customer Service ↔ Inventory Service**:
   - The **Inventory Service** sends **Customer Service** informations about Products.
2. **Customer Service ↔ Tax Service**:
   - The **Tax Service** sends **Customer Service** TaxRates based on the customers State.
3. **OrderManagement Service ↔ Inventory Service**:
   - The **Inventory Service** sends **OrderManagement Service** informations about products and their availabilities.
4. **Sales Service ↔ Customer Service**:
   - The **Customer Service** sends **Sales Service** informations about customers.
5. **Sales Service ↔ Tax Service**:
   - The **Tax Service** sends **Sales Service** TaxRates based on the customers State.
6. **Sales Service ↔ Inventory Service**:
   - The **Inventory Service** sends **Sales Service** infromations about products availabilities.

#### Asynchrounous Communications (RabbitMQ):

1. **Customer Service ↔ OrderManagement Service**:
   - The **Customer Service** publishes an OrderCreatedEvent and **OrderManagement Service** Consumes it to create a new Order.
2. **Customer Service ↔ Sales Service**:
   - The **Customer Service** publishes an OrderCreatedEvent and **Sales Service** Consumes it to create a Ticket.
3. **Customer Service ↔ Employee Service**:
   - The **Customer Service** publishes an OrderCreatedEvent and **Employee Service** Consumes it to assign an employee to the Order.
4. **Inventory Service ↔ Vendor Service**:
   - The **Inventory Service** publishes an LowStockEvent and **Vendor Service** Consumes it to simulate automated restocking (order).
5. **Sales Service ↔ Customer Service**:
   - The **Sales Service** publishes an CustomerRewardsEvent and **Customer Service** Consumes it to update Rewards for customers.

## Function-as-a-Service (FaaS) Component

The **FaaS component** in this architecture is designed to handle low-stock notifications via an Azure Function that is triggered by RabbitMQ. Azure Functions were selected for their seamless integration with .NET and their suitability within a Microsoft-based infrastructure, providing an ideal environment for implementing targeted business logic.

- **Trigger**: The function is triggered by a `LowStockEvent` published by the InventoryService.
- **Task**: The function simulates an email notification by retrieving the respective product ID and quantity and sending a notification to relevant stakeholders. In a real-world scenario, this would involve sending an actual email notification. Additionally, the Vendor Service receives a notification, which could be further extended to initiate automatic reorder processes.
- **Local Execution**: The function can be tested locally without requiring access to the Azure portal, making development and testing more flexible.

This setup allows for scalable, responsive handling of critical inventory events, enhancing the efficiency of stock management processes

## Testing

Before diving into the [documented endpoints](#Microservice-Details) for testing, we've prepared some example payloads to help streamline the process. Since we expanded our CRUD methods and made some adjustments to the models for simplicity and time efficiency, we want to provide essential sample payloads and workflows for testing. Additionally, Swagger offers a user-friendly interface to view and explore valid payloads for each endpoint.

After running the project with docker compose up --build or docker compose up in the terminal, ensure that all services, the gateway, and RabbitMQ are up and running. This may take a few seconds—look for the message confirming that each service is ready: ![Controll connection in Terminal](/README_Files/CheckConnection.png)

This message verifies that RabbitMQ is successfully connected to all services, indicating that the setup is fully operational. If the API Gateway (Ocelot) runs without errors and doesn’t exit unexpectedly, it should be active. You can test it by accessing the Ocelot endpoints at Port 7777.

An impressive workflow our team implemented is the carts/checkout workflow. In this workflow, we leveraged the strengths of our architecture and harnessed the advantages of the chosen technologies to create a real-world, practical scenario. This design showcases the flexibility and scalability of our system, demonstrating how our microservices interact seamlessly to process and manage cart operations through to checkout, just like in a robust production environment.

1. The checkout in Customer Service -> Carts endpoints only triggers if we actually have items in carts.
   For the endpoint `POST http://localhost:7777/carts` use this payload, to add items in the cart:

   ```
   {
     "customerId": 0,
     "items": [
       {
         "productId": 0,
         "quantity": 0
       }
     ]
   }
   ```
2. You should get a `201 Created` response code. Awesome!
   To further check if you added the cart you can use `GET http://localhost:7777/carts/{id}` <- The `cartId` is provided in the response.
   Response Example:
   ![Cart created with 201 response](/README_Files/CartCreatedResponse201.png)
3. Now that you have Items inside your cart, you can checkout otherwise **it will throw an error**. Use endpoint `POST http://localhost:7777/carts/checkout` and provide the `customerId` in the URL or in Swagger in the described field. So if you use the URL it should look like this: `POST http://localhost:7777/carts/checkou?customerId=1`.
4. If its successfull we receive `200 OK` as Status and `All orders placed successfully.` in the response body.

We can watch the different Services communicate and interact with each other as we did this. For this reasons we used a lot of logs in the soure code, so we can debugg quicker and the interactions becomes visible.

For example the Inventory Service is synchrounously communication with the Customer Service so the Customer Service gets the information about the products:

![Synchronous communication between customer service and inventory service](/README_Files/SyncCommCreateCart.png)
At the bottom we see the log that we created the cart with `ProductId` and `UnitPrice`

In Addition we used events as model classes or interfaces for the contracts between services to communicate to each other. After we checking out we get to see RabbitMQ and asynchronous communication in action.

1. The Gateway redirects to the internal API.
![Redirection Gateway to Internal](/README_Files/RedirectToInternalFromGateway.png)
2. The Sales Service received the `OrderCreatedEvent` from RabbitMQ, sends request to the Customer Service to get information about customers. In the same time the order Service also received the event from RabbitMQ.

    ![OrderCreatedEvent received by Order Service and Sales Service](/README_Files/OrderCreatedEventReceived.png)

3. Some lines beneath we can check if the Sales Service received the information that it needs.
Here we go! Now it can calculate the right tax rate which we can see here:
![Sales Service received infromations and sending for calculating tax rates](/README_Files/TaxRateCalculation.png)

4. The Order Management Service created in the same time an order:
![Order Service created the Order](/README_Files/OrderCreated.png)

5. So the Sales Service can create a Ticket:
![Ticket created from Sales Service](/README_Files/TicketCreated.png)

   If we now click again (simulating a confirmation in the UI or in the backend) we can see that the Employee Service assigning an employee based on the State or experience: 
   
   ![Employee Service assigns employee based on State or experience](/README_Files/EmployeeAssigned.png)

In Inventory Service we can use this endpoint `POST http://localhost:7777/test/trigger-low-stock?productId=2&quantity=2` (change the `productId` & `quantity` as you like).
If successfull we see `200 OK` response code with `Low stock event triggered` in the response body.

![Low Stock Event response 200](/README_Files/LowStockEventResponse200.png)

In the Terminal we can check: 
![Low Stock Event received by vendor service](/README_Files/LowStockEventReceived.png)
In that case, the Vendor Service has received the alert from RabbitMQ.

In a real world scenario, we can use this to automate restocking as soon as the stock falls below a defined threshold. In the source code we did a simple implementation, yet the quantity can be chosen freely for testing and simplicity reasons.

```
public async Task CheckStockAndNotify(int productId, int quantity)
        {
            if (quantity < 3) // Define a threshold
            {
                // Directly publish the event using IPublishEndpoint
                await _publishEndpoint.Publish<ILowStockEvent>(new
                {
                    ProductId = productId,
                    Quantity = quantity
                }, context =>
                {
                    context.SetRoutingKey("low_stock"); // Ensure the routing key matches
                });
            }
        }
```

This `LoWStockEvent` is used inside of our faas. We chose Azure Functions because it implements smoothly with .NET and offers a robust Infrastructure. A lot of companies uses Microsoft as the basic Infrastructure for their Servers, Services, Business Tools or even for every thing (even if not, we can still use this service and integrated into our infrastructure). The Azure Function is deployed inside Azure Portal but can be also run locally without needing an Azure account or access to the Azure portal.

1. As described in the [Instructions](#Project-Setup-Steps), Install Azure Functions Core Tools,`cd` to the Function App and run `func start`. You can now watch the Terminal loading, wait until you see it running:

    ![Azure Function Core Tools running](/README_Files/AzurFunctionCoreToolsRunning.png)

2. Trigger **two** times (simulating confirmation) the endpoint as discussed before: `POST http://localhost:7777/test/trigger-low-stock?productId=2&quantity=2`.

3. Now in the Function App environment, you see the output with all neccessarry infromation to simulate an E-Mail sending to all involved stakeholders:

    ![Azure Function receives Event from RabbitMQ](/README_Files/AzureFunctionEventReceived.png)

To get a fast overview about this workflow we visualized it:

![Workflow Overview](/README_Files/Workflow_Overview.png)

The red arrows symbolizes the asynchronous communication (Rabbitmq), the blue the synchronous(HttpClient).

## Microservice Details

### Customer Service

Specific Microservice Port for Customer Service for testing with Swagger - `http://localhost:5000/swagger/index.html`

- **Responsibilities**: Manages customer profiles, shopping carts, rewards, and returns.

  - **`GET http://localhost:7777/customers`**: Retrieve a list of all customers.
  - **`POST http://localhost:7777/customers`**: Create a new customer profile.
  - **`GET http://localhost:7777/customers/{id}`**: Retrieve details of a specific customer by ID.
  - **`PUT http://localhost:7777/customers/{id}`**: Update an existing customer profile by ID.
  - **`DELETE http://localhost:7777/customers/{id}`**: Delete a customer profile by ID.

  ---


  - **`POST http://localhost:7777/carts/checkout`**: Proceed to checkout with the current cart.
  - **`POST http://localhost:7777/carts`**: Create a new cart.
  - **`PUT http://localhost:7777/carts/{cartId}`**: Update an existing cart with the specified cart ID.
  - **`GET http://localhost:7777/carts/{id}`**: Retrieve details of a specific cart by its ID.
  - **`DELETE http://localhost:7777/carts/{id}`**: Remove a cart by its ID.

  ---

  - **`GET http://localhost:7777/giftcards/bycustomer/{customerId}`**: Retrieve gift cards for a specific customer by their ID.
  - **`GET http://localhost:7777/giftcards/{id}`**: Retrieve details of a specific gift card by ID.
  - **`PUT http://localhost:7777/giftcards/{id}`**: Update information for a specific gift card by ID.
  - **`DELETE http://localhost:7777/giftcards/{id}`**: Delete a gift card by ID.
  - **`POST http://localhost:7777/giftcards`**: Create a new gift card.

  ---

  - **`GET http://localhost:7777/items/bycart/{cartId}`**: Retrieve items associated with a specific cart by cart ID.
  - **`GET http://localhost:7777/items/{id}`**: Retrieve details of a specific item by ID.
  - **`PUT http://localhost:7777/items/{id}`**: Update details of a specific item by ID.
  - **`DELETE http://localhost:7777/items/{id}`**: Delete an item by ID.
  - **`POST http://localhost:7777/items`**: Create a new item.

  ---

  - **`GET http://localhost:7777/returns/bycustomer/{customerId}`**: Retrieve returns associated with a specific customer by customer ID.
  - **`GET http://localhost:7777/returns/{id}`**: Retrieve details of a specific return by ID.
  - **`PUT http://localhost:7777/returns/{id}`**: Update information for a specific return by ID.
  - **`DELETE http://localhost:7777/returns/{id}`**: Delete a return by ID.
  - **`POST http://localhost:7777/returns`**: Create a new return.
- **Inter-Service Events**:

  - Publishes a `OrderCreatedEvent` when order is created from the checkout endpoint.
  - Consumes a `UpdateCustomerRewards` after a Sale from the Sales Service.

### Inventory Service

Specific Microservice Port for Inventory Service for testing with Swagger - `http://localhost:5001/swagger/index.html`

- **Responsibilities**: Manages product stock, availability, and triggers low-stock events.

  - **`GET http://localhost:7777/inventory`**: Retrieve a list of all inventory items.
  - **`POST http://localhost:7777/inventory`**: Add a new item to the inventory.
  - **`GET http://localhost:7777/inventory/{id}`**: Retrieve details of a specific inventory item by ID.
  - **`PUT http://localhost:7777/inventory/{id}`**: Update information about a specific inventory item.
  - **`DELETE http://localhost:7777/inventory/{id}`**: Remove an item from the inventory by ID.
  - **`GET http://localhost:7777/inventory/{id}/availability`**: Check the availability of a specific item by ID.
  - **`POST http://localhost:7777/test/trigger-low-stock`**: Trigger a low-stock alert for testing purposes.
- **Inter-Service Events**:

  - Publishes a `LowStockEvent` when stock levels fall below a threshold.

### Sales Service

Specific Microservice Port for Sales Service for testing with Swagger - `http://localhost:5002/swagger/index.html`

- **Responsibilities**: Manages sales transactions, ticket issuance, and customer rewards.

  - **`GET http://localhost:7777/inventorycheck/check-availability`**: Check the availability of inventory items before creating a ticket.

  ---


  - **`GET http://localhost:7777/registers/{id}`**: Retrieve details of a specific register by ID.
  - **`POST http://localhost:7777/registers/open`**: Open a new register.
  - **`PUT http://localhost:7777/registers/close/{id}`**: Close an existing register by ID.

  ---

  - **`POST http://localhost:7777/testrewards/test-rabbitmq`**: Test RabbitMQ message publishing for customer rewards.

  ---

  - **`GET http://localhost:7777/ticket`**: Retrieve a list of all tickets.
  - **`POST http://localhost:7777/ticket`**: Create a new sales ticket.
  - **`GET http://localhost:7777/ticket/{id}`**: Retrieve details of a specific ticket by ID.
  - **`PUT http://localhost:7777/ticket/{id}`**: Update details of a specific ticket by ID.
  - **`DELETE http://localhost:7777/ticket/{id}`**: Delete a ticket by ID.
  - **`GET http://localhost:7777/ticket/check-availability`**: Check ticket availability.
- **Inter-Service Events**:

  - Publishes a `UpdateCustomerRewardsEvent` to update customer rewards.
  - Consumes a `OrderCreatedEvent` to create a Ticket.

### Order Management Service

Specific Microservice Port for Order Management Service for testing with Swagger - `http://localhost:5003/swagger/index.html`

- **Responsibilities**: Manages vendor orders and stock replenishment.

  - **`GET http://localhost:7777/orders`**: Retrieve a list of all orders.
  - **`POST http://localhost:7777/orders`**: Create a new order.
  - **`GET http://localhost:7777/orders/{id}`**: Retrieve details of a specific order by ID.
  - **`PUT http://localhost:7777/orders/{id}`**: Update an existing order by ID.
  - **`DELETE http://localhost:7777/orders/{id}`**: Delete an order by ID.
- **Inter-Service Events**:

  - Publishing  `OrderCreatedEvent` to create an Order.
  - Consumes `OrderCreatedEvent` to create an Order from Customer Service!

### Vendor Service

Specific Microservice Port for Vendor Service for testing with Swagger - `http://localhost:5004/swagger/index.html`

- **Responsibilities**: Manages vendor information and supports automated restocking processes.
  - **`GET http://localhost:7777/vendor`**: Retrieve a list of all vendors.
  - **`POST http://localhost:7777/vendor`**: Add a new vendor to the system.
  - **`GET http://localhost:7777/vendor/{id}`**: Retrieve details of a specific vendor by ID.
  - **`PUT http://localhost:7777/vendor/{id}`**: Update information for a specific vendor by ID.
  - **`DELETE http://localhost:7777/vendor/{id}`**: Delete a vendor by ID.

### Inter-Service Events

- Consumes `LowStockEvent` from `InventoryService` to initiate a reorder process.

### Tax Service

Specific Microservice Port for Tax Service for testing with Swagger - `http://localhost:5005/swagger/index.html`

- **Responsibilities**: Calculates and applies tax rates for transactions.

  - **`GET http://localhost:7777/tax`**: Retrieve all tax rates.
  - **`POST http://localhost:7777/tax`**: Add a new tax rate.
  - **`PUT http://localhost:7777/tax/{id}`**: Update an existing tax rate by ID.
  - **`DELETE http://localhost:7777/tax/{id}`**: Delete a tax rate by ID.
  - **`POST http://localhost:7777/tax/calculate`**: Calculate applicable tax for a transaction.
  - **`GET http://localhost:7777/tax/rate/{state}`**: Retrieve tax rate based on customer’s state.

### Inter-Service Events

- Provides tax calculation results to `SalesService` and `CustomerService` based on the customer’s location.

### Employee Service

Specific Microservice Port for Employee Service for testing with Swagger - `http://localhost:5006/swagger/index.html`

- **Responsibilities**: Manages employee data, including profiles, roles, and store assignments.
  - **`GET http://localhost:7777/employees/{id}`**: Retrieve details of a specific employee by ID.
  - **`PUT http://localhost:7777/employees/{id}`**: Update an existing employee profile by ID.
  - **`DELETE http://localhost:7777/employees/{id}`**: Delete an employee profile by ID.
  - **`GET http://localhost:7777/employees`**: Retrieve a list of all employees.
  - **`POST http://localhost:7777/employees`**: Create a new employee profile.

### Inter-Service Events

- Consumes `OrderCreatedEvent` to assign employees to handle specific orders.

## API Gateway Configuration

The API Gateway (Ocelot) consolidates service endpoints, allowing clients to access all services through a single entry point.

- **Gateway Base URL**: `http://localhost:7777`
- **Example Routes**:
  - `/customers` → CustomerService
  - `/inventory` → InventoryService
  - `/sales` → SalesService

### Ocelot Configuration Example

```json
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/customers",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [{ "Host": "localhost", "Port": 5000 }],
      "UpstreamPathTemplate": "/customers",
      "UpstreamHttpMethod": [ "Get", "Post", "Put", "Delete" ]
    },
    ...
  ],
  "GlobalConfiguration": {
    "BaseUrl": "http://localhost:7777"
  }
}
```


## Conclusion:

This project provides a comprehensive, microservice-based architecture for a Point of Sale (PoS) system, incorporating modern software engineering principles like containerization, distributed databases, and event-driven communication. By breaking down business functions into autonomous services, each with its own database and specific API endpoints, the design ensures flexibility and scalability, allowing individual services to be maintained, updated, and scaled separately.

The usage of RabbitMQ for asynchronous, event-driven communication illustrates how event-based architectures may help in decoupling, allowing services to respond to real-time changes without being overly dependent. The use of synchronous communication via.NET's HttpClient facilitates essential, real-time interactions between services that demand immediate responses. Together, these ideas demonstrate how hybrid communication strategies can be used to build a responsive and robust system.

The Ocelot API Gateway serves as a common entry point, simplifying client access to different services and improving security by centralizing API request handling. Furthermore, delivering the project with Docker and Docker Compose creates a consistent runtime environment across several platforms, minimizing the difficulty of deploying microservices in both local and cloud contexts.

The Function-as-a-Service (FaaS) component, built with Azure Functions, offers a layer of serverless computing that works seamlessly with the.NET ecosystem and RabbitMQ. This FaaS example shows how serverless functions may perform specific tasks—such as low-stock warnings and notifications—in a cost-effective and scalable manner, giving flexibility to the overall architecture.

This PoS system is designed to address real-world business requirements using a microservices architecture, particularly in high-demand retail settings where each service, such as inventory management, order processing, and customer engagement, can function independently. The modular design allows the system to expand and adapt to new requirements, as well as integrate additional business logic, without requiring extensive reorganization.

In conclusion, this project provides an effective blueprint for a distributed, event-driven PoS system. It strikes a balance between service independence and cohesive interactions, ensuring scalability, resilience, and maintainability—all of which are required for modern software systems that must handle complex, changing business needs.


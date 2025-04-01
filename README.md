
---

### Setting Up the Project

1. **Clone the Project**  
   Start by cloning the repository to your local machine.

2. **Open the Project in Visual Studio**  
   Open the solution file in Visual Studio.

3. **Set Startup Project**  
   Right-click on the `AirportControlTower.API` project in the Solution Explorer and select **Set as StartUp Project**.

4. **Open NuGet Package Manager Console**  
   - From the menu bar, click on **Tools**.
   - Select **NuGet Package Manager**, then choose **NuGet Package Manager Console**.

5. **Select the Default Project**  
   - In the NuGet Package Manager Console, close to the **Default project** dropdown, select `AirportControlTower.Infrastructure`.

6. **Run the Migrations**  
   - In the console, type the following command and press Enter:  
     ```
     add-migration initial
     ```
   - This will create the necessary migration for your database.

7. **Update the Database**  
   - After the migration is complete, run the following command to apply the migration and create the database:  
     ```
     update-database
     ```

8. **Set Docker-Compose as Startup Project**  
   - Right-click on the `docker-compose` file in Solution Explorer and select **Set as StartUp Project**.

9. **Run the Application**  
   - Click on the **Run** button (the play icon) to start the project.

10. **Access the pgAdmin Interface**  
    - Open your browser and go to:  
      ```
      http://localhost:5050/
      ```
    - **Login** with the following credentials:  
      - **Username**: `pgadmin@pgadmin.org`  
      - **Password**: `admin`

11. **Set Up Server in pgAdmin**  
    - After logging in, click **Set Up Server** to open the server setup modal.
    - Enter the following details:  
      - **Name**: Local (or any name you prefer)
      - **Connection Tab**:  
        - **Host name/address**: `host.docker.internal`  
        - **Port**: `5432`  
        - **Maintenance database**: `postgres`  
        - **Username**: `admin`  
        - **Password**: `adminamin`
    - Click **Save**.

12. **Explore the Database**  
    - Once the server is added, click on the server name (`Local` or your chosen name) to expand it.
    - Under **Databases**, find and open `airport_control_tower`.
    - Under **Schemas**, open **public** then **Tables**.
    - You should see the database tables. Right-click on any table and select **View/Edit Data** to view all the data in the table.

13. **Postman Collection**  
    - I've also published my Postman collection used for testing. You can use it for testing the API. The collection is available in the email I sent.

14. **Dashboard Project**  
    - The dashboard is a separate project. When you clone and run it, you'll be asked to log in.
    - The default login credentials are:  
      - **Username**: `test@example.com`  
      - **Password**: `password`

---

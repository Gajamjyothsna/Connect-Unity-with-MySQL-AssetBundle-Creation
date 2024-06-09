# Connect-Unity-with-MySQL-AssetBundle-Creation
This is a Project where Unity is connected with MySQL database using Local Server. On Addition to it, Created AssetBundles and Loaded AssetBundles in Unity. I have done this project by Connecting Unity -> PHP -> MySQL.

## **SetUp The Project**

- ### **Install MAMP**
    Install the Mamp from Offical Website (https://www.mamp.info/en/windows/).
    In this Application, the MAMP is having Apache Server and having default port.
- ### **Install MySQL**
  Install the MySQL from website (https://dev.mysql.com/downloads/connector/odbc/)
- ### **Install PHP**
  Install the PHP from website (https://www.php.net/downloads.php). Set the environment Varaiables

## **Creating DataBase**
1. You can create Database uses MySQL workbench or PHP admin. But I have used command prompt for creating databases. Because, it is famiilar for me.
2. Go to the MySQL path in Command Promot with this command ``` cd /usr/local/mysql/bin ```
3. Log into Database with command ``` mysql -u root -p ``` Replace root with your MySQL username. You'll be prompted to enter your MySQL password.
4. Now, Create the database with command ``` CREATE DATABASE foobar; ```. Replace foobar with your database name.
5. You can confirm your database was created by running ```SHOW DATABASES:```
```
SHOW DATABASES;
+--------------------+
| Database           |
+--------------------+
| foobar             |
| information_schema |
| mysql              |
| performance_schema |
| sys                |
+--------------------+
5 rows in set (0.01 sec)
```

## **Advance Topic Reference**
We can do this project with using ODBC or the MySQL .Net Connector plugin. (https://github.com/Uncle-Uee/mysql-unity)





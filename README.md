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
mysql> SHOW DATABASES;
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
6. Select into your database by running
```   
mysql> USE foobar;
Database changed
```
7. Let’s create a demo table called users with two fields, id and email:
```   
mysql> CREATE TABLE `users` ( 
    `id` INT NOT NULL AUTO_INCREMENT, 
    `email` VARCHAR(255), 
    PRIMARY KEY (`id`)
);
Query OK, 0 rows affected (0.01 sec)
```
8. We can view the structure of our new table:
```
mysql> DESCRIBE users;
+-------+--------------+------+-----+---------+----------------+
| Field | Type         | Null | Key | Default | Extra          |
+-------+--------------+------+-----+---------+----------------+
| id    | int          | NO   | PRI | NULL    | auto_increment |
| email | varchar(255) | YES  |     | NULL    |                |
+-------+--------------+------+-----+---------+----------------+
2 rows in set (0.00 sec)
```
9. And add a couple rows:
```    
mysql> INSERT INTO users (`email`) VALUES ('mail@codewithsusan.com'), ('test@user.com');
Query OK, 2 rows affected (0.01 sec)
Records: 2  Duplicates: 0  Warnings: 0
```
10. And view the new rows:
```    
mysql> SELECT * FROM users;
+----+------------------------+
| id | email                  |
+----+------------------------+
|  1 | mail@codewithsusan.com |
|  2 | test@user.com          |
+----+------------------------+
2 rows in set (0.00 sec)
```

## **Test from web app**
Finally, below is some barebones PHP code to test your database connection from a web application.
```
<?php
# Credentials
$database = 'foobar';
$username = 'foobarAdmin';
$password = 'your-password-here';

# Establish connection
try {
    $conn = new PDO('mysql:host=localhost;dbname='.$database, $username, $password);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    echo 'Connected successfully<br>';
} catch(PDOException $e) {
    echo 'Connection failed: ' . $e->getMessage();
}

# Test retrieving data
$sql = 'SELECT * FROM users';
$statement = $conn->query($sql);
$data = $statement->fetchAll();
foreach($data as $row) {
    echo 'id: ' . $row['id'] . ', email: ' . $row['email'].'<br>';;
}
```
If everything is set up correctly, the above code should produce output that looks like this:



## **Advance Topic Reference**
We can do this project with using ODBC or the MySQL .Net Connector plugin. (https://github.com/Uncle-Uee/mysql-unity)





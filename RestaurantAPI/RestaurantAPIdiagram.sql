CREATE DATABASE RestaurantDB;
GO

USE RestaurantDB;
GO

CREATE TABLE [categories] (
  [id] int PRIMARY KEY,
  [category_id] uniqueidentifier UNIQUE NOT NULL,
  [name] varchar(50) UNIQUE NOT NULL
)
GO

CREATE TABLE [menu_items] (
  [id] int PRIMARY KEY,
  [menu_item_id] uniqueidentifier UNIQUE NOT NULL,
  [name] varchar(100) NOT NULL,
  [description] varchar(100),
  [price] decimal(10,2) NOT NULL,
  [category_id] int,
  [meal_type] varchar(50) NOT NULL,
  [created_at] datetime,
  [updated_at] datetime,
  [deleted_at] datetime
)
GO

CREATE TABLE [orders] (
  [id] int PRIMARY KEY,
  [order_id] uniqueidentifier UNIQUE NOT NULL,
  [user_id] int,
  [order_date] datetime NOT NULL,
  [status] varchar(50) NOT NULL,
  [total_amount] decimal(10,2) NOT NULL,
  [created_at] datetime,
  [updated_at] datetime,
  [deleted_at] datetime
)
GO

CREATE TABLE [order_items] (
  [id] int PRIMARY KEY,
  [order_item_id] uniqueidentifier UNIQUE NOT NULL,
  [order_id] int,
  [menu_item_id] int,
  [quantity] int NOT NULL,
  [price] decimal(10,2) NOT NULL,
  [created_at] datetime,
  [updated_at] datetime,
  [deleted_at] datetime
)
GO

CREATE TABLE [payments] (
  [id] int PRIMARY KEY,
  [payment_id] uniqueidentifier UNIQUE NOT NULL,
  [order_id] int,
  [amount] decimal(10,2) NOT NULL,
  [method] varchar(50) NOT NULL,
  [status] varchar(50) NOT NULL,
  [paid_at] datetime
)
GO

CREATE TABLE [reservations] (
  [id] int PRIMARY KEY,
  [reservation_id] uniqueidentifier UNIQUE NOT NULL,
  [table_id] int,
  [user_id] int,
  [reservation_time] datetime NOT NULL,
  [guest_count] int NOT NULL,
  [status] varchar(50) NOT NULL,
  [created_at] datetime,
  [updated_at] datetime,
  [deleted_at] datetime
)
GO

CREATE TABLE [tables] (
  [id] int PRIMARY KEY,
  [table_id] uniqueidentifier UNIQUE NOT NULL,
  [table_number] int UNIQUE NOT NULL,
  [seats] int NOT NULL,
  [status] varchar(50) NOT NULL
)
GO

CREATE TABLE [roles] (
  [id] int PRIMARY KEY,
  [role_id] uniqueidentifier UNIQUE NOT NULL,
  [name] varchar(50) UNIQUE NOT NULL,
  [description] varchar(50)
)
GO

CREATE TABLE [users] (
  [id] int PRIMARY KEY,
  [user_id] uniqueidentifier UNIQUE NOT NULL,
  [first_name] varchar(50) NOT NULL,
  [last_name] varchar(50) NOT NULL,
  [username] varchar(50) UNIQUE NOT NULL,
  [password_hash] varchar(255) NOT NULL,
  [email] varchar(100) UNIQUE NOT NULL,
  [role_id] int,
  [created_at] datetime,
  [updated_at] datetime,
  [deleted_at] datetime
)
GO

CREATE INDEX [menu_items_index_0] ON [menu_items] ("category_id")
GO

CREATE INDEX [menu_items_index_1] ON [menu_items] ("meal_type")
GO

CREATE INDEX [orders_index_2] ON [orders] ("order_date")
GO

CREATE INDEX [orders_index_3] ON [orders] ("user_id")
GO

CREATE INDEX [orders_index_4] ON [orders] ("status")
GO

CREATE INDEX [order_items_index_5] ON [order_items] ("order_id")
GO

CREATE INDEX [order_items_index_6] ON [order_items] ("menu_item_id")
GO

CREATE INDEX [payments_index_7] ON [payments] ("order_id")
GO

CREATE INDEX [payments_index_8] ON [payments] ("status")
GO

CREATE INDEX [reservations_index_9] ON [reservations] ("reservation_time")
GO

CREATE INDEX [reservations_index_10] ON [reservations] ("user_id")
GO

CREATE INDEX [reservations_index_11] ON [reservations] ("status")
GO

CREATE INDEX [tables_index_12] ON [tables] ("status")
GO

CREATE UNIQUE INDEX [users_index_13] ON [users] ("email")
GO

CREATE UNIQUE INDEX [users_index_14] ON [users] ("username")
GO

ALTER TABLE [menu_items] ADD FOREIGN KEY ([category_id]) REFERENCES [categories] ([id])
GO

ALTER TABLE [orders] ADD FOREIGN KEY ([user_id]) REFERENCES [users] ([id])
GO

ALTER TABLE [order_items] ADD FOREIGN KEY ([order_id]) REFERENCES [orders] ([id])
GO

ALTER TABLE [order_items] ADD FOREIGN KEY ([menu_item_id]) REFERENCES [menu_items] ([id])
GO

ALTER TABLE [payments] ADD FOREIGN KEY ([order_id]) REFERENCES [orders] ([id])
GO

ALTER TABLE [reservations] ADD FOREIGN KEY ([table_id]) REFERENCES [tables] ([id])
GO

ALTER TABLE [reservations] ADD FOREIGN KEY ([user_id]) REFERENCES [users] ([id])
GO

ALTER TABLE [users] ADD FOREIGN KEY ([role_id]) REFERENCES [roles] ([id])
GO

INSERT INTO roles (role_id, name, description)
VALUES
(NEWID(), 'Employee', 'Regular employee'),
(NEWID(), 'Manager', 'Restaurant Manager'),
(NEWID(), 'Admin', 'System Administrator');

INSERT INTO categories (category_id, name)
VALUES 
(NEWID(), 'Oriental'),
(NEWID(), 'Italian'),
(NEWID(), 'French');

INSERT INTO users (
    user_id,
    first_name,
    last_name,
    username,
    password_hash,
    email,
    role_id,
    created_at
)
VALUES (
    NEWID(),
    'Admin',
    'User',
    'admin',
    'AQAAAAIAAYagAAAAELTOP5aCPzzILQ0wo55bEUG/+3IN5NudlSRk9MQaL5P0Zpa4tHEiOVUsTYVzpjp5qg==', --"Admin1234" 
    'admin@example.com',
    (SELECT id FROM roles WHERE name = 'admin'),
    GETDATE()
);
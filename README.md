# DrugHouse
An Patient management tool build using C#, WPF, Entity framework.

## Introduction

For now, DrugHouse provides functionalities for :
Add/Edit/Delete Patient record
Store/retrive Drug information
Add/Edit Diagnosis information
Report generation

Basic idea:
Whenever a patient is admitted or visits the hospital, an "Case" is created. Each paitent record can contain multiple Cases. 
A Case can be of two types:
1. Visit [Out-patient]
2. Admittance [In-patent]


## Build and Run
This application built on Code First model of Entity Framework.

Edit the connection string in app.config file for point it to your SQL Server instance.
When the applictation is run for the first time, the required database will be created automatically. 

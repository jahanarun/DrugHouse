# DrugHouse
An Patient management tool build using C#, WPF, Entity framework.

## Introduction

For now, DrugHouse provides functionalities for :
Add/Edit/Delete Patient record
Store/retrive Drug information
Add/Edit Diagnosis information
Report generation

Basic idea:
Whenever a patient is admitted or visits the hospital, an "Case" is created. Each paitent record contain multiple Cases. 
A Case can be of two types:
1. Visit [Out-patient]
2. Admittance [In-patent]


## Setup
This application uses Entity Framework v6 - Code First. At least, SQL server Express edition is required.
The first time the application run, it creates the required database automatically. But you need to edit the connection string in app.config file.

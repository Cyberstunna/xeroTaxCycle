# XeroTaxCycle
C# Web application presenting exceptions in Xero's Products
![Xero x Jarvis](readme.png)

## My Solution Mind Map
The DataFile.xml issued was extremely rich in data detailing exception errors in different applications over a period of time. Given the nature of data, there are ample ways it can be utilized to give insight into important processes thus helping the user review the data more efficiently — the challenging part of this exercise was figuring out which insights to include.

Depending on what the user would be reviewing data for, the different parts of the data that are presented can vary. For my use case however, I priorotized reviewing the dataset for understanding what products, versions and bugs are affecting our Customer experience the most and where to start tackling them.

## Solution Requirements
- The User would need to be able to view the sorted data in a simple form e.g. A Table.
- The User would need to be able to visualize the data by means of charts or graphs because they help to express complex data in a simple format. 
- The User would need to be able to search the data.
- It would be beneficial for the User to be able to get summaries of the different components of the data being presented.

## Solution Reflection
While I would have loved to create a J.A.R.V.I.S like dashboard, the time constraint didn't allow for it. Here are a couple things I would have done differently if I had more time to work on the assesment.
- I would have included pagination and filtering functionality for the tabled data.
- I would have included a tab detailing the top 5 customers with the most reports. This will help the User assist the more pained customers first and trickle down.
- I would have included some "over time" visualization of exceptions for that year (2018 I believe).
- I would have derived a relationship between the DoxCycle and TaxCycle products by means of a Python analysis which would help the User understand why the earlier has less exceptions than the latter. This would help implement an "Analysis Suggestions" section which could potentially help the development team take a better course of action in future releases.

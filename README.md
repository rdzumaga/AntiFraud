# AntiFraud

### This is an example app acting as a purchase system with anti-fraud validation.

System is built using .net core on backend + angular 8 on frontend. Persistance is done with EF Core backed by SQLite (file or memory based). Hangfire is used as background job processor, NSwag is used to generate swagger/openapi doc for API and NSwagStudio to generate typescript client.

### Storage config

 Based on confitional compilation symbol 'USE_IN_MEMORY_SQLITE' in-memory databases are used (both for hangfire and for app db itself). If that symbol is not set, then file storage is used, file connection strings are specified in appsettings.json. For existing file-based databases running might need to be run.

### App config
Application uses standard appsettings.json. Important settings are: connection strings (see storage config section) and 'CheckPurchasesJobCron'. -> this field specifies how often a job is started to find and process new purchases. Cron expression is expected.  Aside from that, SMTP settings have to specified (not really, see note below :)

### Note on Email notificiations
App is setup to notifiy users/support about purchases (and frauds) via email, but no actual email implementation is done. In order to monitor notifications, every would-be email is insted printed as a short Info on default logger (console) (see EmailHelper.cs).

### Anti fraud
Currently two anti fraud filters are set.
* NigerianPrince: this filter detects fraud if:
   * coutry of origin is Nigeria
   * amount is over 1000
   * this is first order for that email
* UnusuallyHighAmount: this filter detects fraud if:
   * amount is 5 times bigger than average amount of existing valid purchases

### Data vailidation
Front-end code does user-side validation for entire form, backend for now only validates email and amout value

### Angular app
App is composed of 3 components
* Main form to send purchase order
* Viewing order by Id
* List of all orders with urls for convienience

Also hangfire dashoard is enabled to easily monitor & (if needed) schedule jobs on demand)


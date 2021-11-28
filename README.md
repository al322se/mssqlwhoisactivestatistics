# mssqlwhoisactivestatistics
Very simple automation of WhoIsActive stored procedure for performance analytics of MS-SQL.

# How it works
It create [sp_whoisactive](https://github.com/amachanic/sp_whoisactive) stored procedure on database if not exist. Than it execute the stored procedure every `DelayTimespan` (by default 10 secods), and store results in [LiteDb](https://github.com/mbdavid/LiteDB) for further analysis.


## Configuration parameters 
* 'ConnectionString' - connections string to database.
* 'DelayTimespan' - delay for execution 
* 'DbName'- name of file for results in LiteDb format

## How to use

Set connection string to your server. Start load tests or some another performance. Start the utility to collect WhoIsActive results.
Aftewards open results in [LiteDb studio](https://github.com/mbdavid/LiteDB.Studio) and analyse. The most interesting columns are sql_text, wait_info, query_plan, locks.
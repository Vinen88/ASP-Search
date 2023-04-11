# ASP-Search

ASP.NET core Elasticsearch web based search engine

# data

Data from [Data.gov](data.gov)

# useful link

- [Elasticsearch and NEST (.NET Client) Cheatsheet](https://github.com/mjebrahimi/Elasticsearch-NEST-CheatSheet-Tutorials/blob/master/README.md)

# Features

## Basic

 - [x] The website should load and index some randomly generated or selected test data.
 - [x] Your test data can be anything you want but should be at least several thousand records.
 - [x] The app should allow the user to enter keywords and find your indexed content.
 - [x] Your app should also feature a responsive design to ensure it renders well for a variety of screen sizes and resolutions.

## Advanced

 - [x] Stemming should be used – for example, when searching for “engineer”, the search should also return results for “engineering”, “engineers”, and “engineered”.
 - [x] Spell checking should present corrected search terms for user misspellings.
 - [ ] Related search terms could be displayed when a user has searched, suggesting alternate keywords related to what they’ve already searched for.
 - [ ] Auto-complete should suggest search terms as the user types in the search box.
 - [ ] Attach meta-data to your indexed records and provide faceting options for the user to filter by.

# Known Issues

### Data

 - Data isn't very unique there is alot of repitition. Better data could be found and plugged in I guess?

### Search

- Results limited to 20
- no pagnation, WIP
- slow start up, not worth fixing as test data loads in.
- results could probably be in a table instead of a list

### Docker

- need to dockerize ASP.NET core app 
- elasticsearch docker container is quite memory intensive. (around 7GB)

### choices made to make demos easier

- no persistant elastic search data. 
- data is rebuilt every time program is run
- only 2100 entries, can add more by reformatting data with the python file but start up times increase quite a bit

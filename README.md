# ASP-Search

ASP.NET core Elasticsearch web based search engine

# To Run

### Start Elasticsearch

 - BE SURE TO START THIS FIRST!
 - navigate to folder with docker-compose.yml in it (should be the main folder)
 - ```docker-compose up```
 - give it time to run takes 2-5 minutes to start for me on an older windows machine

### Start website
 
 - For simplicity open the .sln file in Visual Studio
 - run without debugging
 - will take about a minute to start as it has to load data into Elasticsearch

### Suggested Search Terms
 - C-Max
 - Tesla
 - seatle
 - WA

These should get you enough info and you can have fun after that, data is all electric car data so try searching anything related.

# Data

Data from [Data.gov](data.gov)

# Useful Links

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

 - Data isn't very unique there is alot of repitition. Better data could be found and models adjusted I guess?

### Search

- could be bugs with pagination, it was a last minute addition but from my testing it's working
- slow start up, not worth fixing as test data loads in.

### Docker

- need to dockerize ASP.NET core app Not sure if that would make running it any easier due to HTTPS certs.
- elasticsearch docker container is quite memory intensive. (around 7GB)

### Choices Made to Make Demo Simpler

- no persistant elastic search data. 
- data is rebuilt every time program is run
- only 2100 entries, can add more by reformatting data with the python file but start up times increase quite a bit

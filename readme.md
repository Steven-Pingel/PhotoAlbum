# Photo Album Tech Showcase

### How to run:
1. Build the docker image
   1. `docker build . -t photoalbum`
2. Run the image in interactive mode
   1. `docker run -it photoalbum`
3. Follow prompts in console application

### Further Considerations
* Could expand error handling / fault tolerance
* Could revisit mocking IRestClient to create unit tests instead of integration tests
* The level of splitting the domain from implementations and stuff is a little contrived but this is a demonstration after all

### Libraries/Packages Utilized
* RestSharp
* Xunit
* Spectre.Console
* Fluent Assertions


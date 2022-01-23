# John's Farm
## Inspiration
We were inspired by the man John Deere himself! His hard work for the farming industry inspirited us to create our very own farming game, called John's Farm, to commemorate him.

## Purpose
We wanted to create a fun, yet challenging, strategy game to simulate grrowing crops on a farm. 

## How we built it
We designed our webpage with HTML, CSS, and Javascript. With python's Django framework, we served a SQLite database, which stores all the information for user login/registration and PeteCoin leaderboards. To query results from the SQLite database, we configured Django functions to read and write data to the local database.

## Challenges
With the specificity needed for our database queries needed, we had to dive deeper into the complexities of our database relations and learn how to manipulate the data to obtain the information that we needed. Additionally, we wanted our user's information to be secure; however, that required us to implement security measures on the database and login system, which we did not know how to use before. We learned how to applied the Django authentication system to hash passwords and keep the database secure.

## Accomplishments and what we learned
We are proud of the UI that we created for the webpage and the leaderboard; we went for a simplistic design for both the home page and profile page, and we love how it turned out. In addition, we are proud to have implemented a SQLlite database to configure a secure login page and a dynamic site that refreshes the points leaderboard. As this is the first hackathon for the majority of our group, we learned a lot about the process of a hackathon and how to realize our ideas into code. Functionality wise, we learned more about web dev and introduction of the Django backend for most members of the group.

## What's next
We would like to host the site in the cloud so that it is accessible to everyone (currently, the site can only be run on the local machine). Additionally, we want to add functionality that generates favors after each league ends to add an extra level to the challenge. We would also like to develop an updated and improved UI for the Login/Register, League selector and Challenge Selector pages and a system that verifies if contestants complete challenges.


## Built With
Python Django, HTML, CSS, Javascript, SQLite Database

# GitHubApp Alter Domus Task

## Table of Contents
* [Requirements](#requirements)
* [About the Project](#about-the-project)
* [Prerequisites](#prerequisites)
* [How to Run](#how-to-run)
* [Suggestions for Improvement](#suggestions-for-improvement)
* [Author](author)

## Requirements
Alter Domus task:

<h3>Scenario<h3>
<p>Your line manager proposed the development of a new web page, where team developers can
inform their GitHub id (login) and receive advices about how they can improve their own profiles.
On the sprint plan, the following user stories were assigned to you:</p>

<p>* As a developer, I want to be informed if my profile does not have a biography, company and
location filled, so that I can feed these values and keep my profile completed.
Acceptance Criteria: Given my profile login on the web page input, when GitHub user
properties “bio”, “company” and “location” are null/empty, then notify me to update those
fields on my profile.</p>

<p>* As a developer, I want to be informed if I’m following less than five users or have less than
five public repositories on my account, so that will engage me to look for other developers’
activities and be more active on network and projects.
Acceptance Criteria: Given my profile login on the web page input, when GitHub user
properties “following” and “public_repos”</p>

<p>* You will be the only developer working on this user stories and eventually your code will be
maintained by another team. The project has sprints of ten days and it’s expected that you can finish
both stories in the same sprint.</p>
  
  What will be evaluated
* Research skills
* Clear and maintainable code skills
* Documentation (on code and outside of it)
* OOP Concepts
* Exceptions handling
* Right usage of HTTP Protocol
* Creativity
* Look and feel (user experience)
* Functionalities aligned with requirements
* Suggestions of improvement
* Unit/Integration Tests, Log implementation and right the usage of IOC, AutoMapper and
other industry standard Libs will be considered as a plus.
  
## About the Project
- This project was built using ASP.NET MVC framework
- Profile page is the start main page and has a search which brings and displays Git Hub user profile data "avatar", "login", “bio”, “company” and “location” and if “bio”, “company” and “location” are null/empty it creates a new element on the page with a notification message asking the user to fill in those details.
- Repositories page has a search which brings and displays Git Hub user repositories data and if there are less than 5 repositories a new element is created with a notification message asking the user to create more projects.
- Followers page has a search which brings and displays Git Hub user followers data and if there are less than 5 followers a new element is created with a notification message asking the user to create more projects.
- Each page has a navigation bar to navigate within the pages.

## How to Run

- Download the solution from the repository to your local machine.
- Open the solution using Microsoft Visual Studio 2022.
- Click on the "Green play button" GitHubApp to start.

## Prerequisites
* Microsoft Visual Studio 2022
* .NET 6.0
* NuGet package - Newtonsoft.Json 13.0.1

## Suggestions for Improvement

- Design could be improved (style, responsive design)
- More data from each API call could be displayed on each page

## Author

<strong>Erikas Pomeliaika</strong>

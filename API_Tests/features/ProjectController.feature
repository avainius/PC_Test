Feature: ProjectController
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Create a new project
	Given I have a new project ready
	When I post the new project to create it
	Then A new project is added to the project list

Scenario: Try to register an existing project again
	Given I have a new project ready
	And There already is a project like the new one registered
	When I post the new project to create it
	Then An exception is thrown

Scenario Outline: Try to assign a project to an existing user
	Given I have an existing user with ID: <userId>
	And I have an existing project with ID: <projectId>
	When I post to assign user <userId> to project <projectId>
	Then The user <userId> gets assigned to the project <projectId>

	Examples: 
	| userId | projectId |
	| 1      | 1         |
	| 1      | 3         |

Scenario Outline: Try to assign a project to a non-existing user
	Given I have an existing project with ID: <projectId>
	When I post to assign user <userId> to project <projectId>
	Then An exception is thrown

	Examples: 
	| userId | projectId |
	| 11     | 1         |
	| 10     | 1         |
	
Scenario Outline: Try to assign a non-existing project to an existing user
	Given I have an existing user with ID: <userId>
	When I post to assign user <userId> to project <projectId>
	Then An exception is thrown

	Examples: 
	| userId | projectId |
	| 1     | 1         |
	| 1     | 1         |

Scenario: Try to retrieve my projects with supplied token information
	Given I have a registered user
	And The registered user has projects assigned to him
	And The registered user is in the data storage
	And The registered user has supplied a token
	When I try to get that users data
	Then I receive a list of projects assigned to that user

	
Scenario: Try to retrieve my projects without supplied token information
	Given I have a registered user
	And The registered user has projects assigned to him
	And The registered user is in the data storage
	And The registered user has not supplied a token
	When I try to get that users data
	Then I receive a null list of projects assigned to that user
	
Scenario: Try to get all projects with an employee user
	Given I have a registered user
	And I have projects registered in the database
	And The registered user has supplied a token
	When I try to get all projects
	Then I get all existing projects
Feature: HomeController
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario Outline: A new user is registered
	Given I have a new user with an email:<email> and password:<psw> ready for registration
	When I post a registration request for the new user 
	Then A new user is created
	And No Exception was thrown

	Examples:
	| email          | psw  |
	| test@gmail.com | 0000 |

Scenario Outline: Trying to register a user with null credentials
	Given I have a new user with an email:<email> and password:<psw> ready for registration
	When I post a registration request for the new user
	Then User is not registerd
	And  An exception was thrown

	Examples:
	| email          | psw  |
	|                | 0000 |
	| test@gmail.com |      |

Scenario: Trying to login with existing user
	Given I have an existing user
	When I login
	Then I get an authentication token
	And The token is valid
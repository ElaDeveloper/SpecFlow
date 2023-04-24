Feature: This feature ensures that all the operations involved in accounts are verified


Scenario: Add a new account newly in sales hub app and ensure its deleted
	Given User launches MS Dynamics CRM application and navigates to Sales hub Menu
	When User adds a new account to the system
	Then User should be able to view the newly added account
	
	Scenario: Add a new case verify the data add to a queue and assign to a team
	Given User launches MS Dynamics CRM application and navigates to Sales hub Menu
	And User adds a Case to the system
	When User validates the case attributes then adds the Case to the queue and assigns to a user
	Then The case should be added to the queue and assigned to the user
	And User deletes the case from the system
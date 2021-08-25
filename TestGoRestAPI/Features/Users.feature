Feature: UsersFeature

@DeleteUser
Scenario Outline: Verify user is created
	Given user with email "testuser11@test.ts" is absent
	When create user
	Then verify user is created

@RequiresUser
Scenario: Verify user email is updated
	Given user with email "testuser11@test.ts" is present
	When partially update user with email "testuser11@test.ts"
	Then verify user is updated

@RequiresUser
Scenario: Verify user is updated
	Given user with email "testuser11@test.ts" is present
	When update user with email "testuser11@test.ts"
	Then verify user is updated

@RequiresUser
Scenario: Verify user is deleted
	Given user with email "testuser11@test.ts" is present
	When delete user with email "testuser11@test.ts"
	Then verify user is deleted

@RequiresUser
Scenario: Verify todo is created
	Given user with email "testuser11@test.ts" is present
	When user with email "testuser11@test.ts" creates todo
	Then verify todo is created

@RequiresUser @RequiresTodo
Scenario: Verify todo is updated
	Given user with email "testuser11@test.ts" is present
		And todo with title "Sign up for course" is present
	When update user`s todo partially
	Then verify todo is updated

@RequiresUser @RequiresTodo
Scenario: Verify todo is deleted
	Given todo with title "Sign up for course" is present
	When delete user`s todo
	Then verify todo is deleted

Scenario: Verify page number
	Given page number "10" of todos is loaded
	When go to next page
	Then verify the page is loaded
Feature: EcommerceCartCheckoutFunctionality

A short summary of the feature
Background: 
Given I am logged in to the ecommerce site 
And I have added a 'Hoodie with Pocket' to my cart 

@GUI 
Scenario: Checking Discount Applied
	When I input the coupon 'nfocus' and click apply
	Then A discount of 25% is applied to my cart
	And The order total updates accordingly 
	#maybe add a table, for option for nfocus coupon.

@GUI
Scenario: Checking The Order Has Been Placed
	When I proceed to checkout
	And Fill in my address and click the payment by cheque option 
	Then I can place my order and see a summary of my order including an order number 
	And Verify my order has been placed my checking the same order number appears on the orders page 
	
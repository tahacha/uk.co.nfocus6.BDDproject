@GUI
Feature: EcommerceCartCheckoutFunctionality

A short summary of the feature
Background: 
Given I am logged in to the ecommerce site 
And I have added a 'Cap' to my cart 

@CheckDiscount
Scenario: Checking Discount Applied
	When I input the coupon '<coupon>' and click apply
	Then A discount of <discount>% is applied to my cart
	And  The order total updates accordingly 
Examples:
	| coupon    | discount |
	| edgewords | 10       |
	| nfocus    | 25       |

@CheckOrderNo
Scenario: Checking The Order Has Been Placed
	When I proceed to checkout
	And Fill in my address and click the payment by cheque option 
	Then I can place my order and see a summary of my order including an order number 
	And Verify my order has been placed my checking the same order number appears on the orders page 
	
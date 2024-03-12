@GUI
Feature: EcommerceCartCheckoutFunctionality
In order to promote the ecommerce site
checkout functionality 
must be tested

Background: 
Given I am logged in to the ecommerce site 
  And I have added a 'Sunglasses' to my cart 

@CheckDiscount 
Scenario: Checking Discount Applied
	When I input the coupon '<coupon>'
	Then A discount of <discount(%)>% is applied to my cart
	  And  The order total updates accordingly
Examples:
| coupon    | discount(%)|
| nfocus    | 25         |
| edgewords | 15         | 

@CheckOrderNo
Scenario: Checking The Order Has Been Placed
	When I proceed to checkout
	  And Fill in my address
	Then I can place my order
	  And Verify my order has been placed by checking the orders page 
	
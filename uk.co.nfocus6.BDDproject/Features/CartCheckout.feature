@GUI
Feature: EcommerceCartCheckoutFunctionality
In order to promote the ecommerce site
checkout functionality 
must be tested

Background: 
Given I am logged in to the ecommerce site 
  And I have added a 'Sunglasses' to my cart 

@CheckDiscount 
Scenario Outline: Checking Discount Applied
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
	  And Fill in my billing details with
	      | Field     | Value               |
	      | FirstName | Smith               |
	      | LastName  | Jones               |
	      | Country   | United Kingdom (UK) |
	      | Street    | 1 Oxford Street     |
	      | City      | London              |
	      | Postcode  | W1B 3AG             |
	      | Phone     | 123456789           |
	Then I can place my order
	  And Verify my order has been placed by checking the orders page 
	
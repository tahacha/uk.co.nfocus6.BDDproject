@GUI
Feature: EcommerceCartCheckoutFunctionality
In order to promote the ecommerce site
the cart and checkout functionality 
must be tested

Background: 
Given I am logged in to the ecommerce site 
And I have added a 'Tshirt' to my cart 

@CheckDiscount 
Scenario: Checking Discount Applied
	When I input the coupon 'edgewords' and click apply
	Then A discount of 15% is applied to my cart
	And  The order total updates accordingly 
#Examples: 
#| coupon    | discount |
#| edgewords | 15       |
#| nfocus    | 25       |

@CheckOrderNo
Scenario: Checking The Order Has Been Placed
	When I proceed to checkout
	And Fill in my address and click the payment by cheque option 
	Then I can place my order and see a summary of my order including an order number 
	And Verify my order has been placed my checking the same order number appears on the orders page 
	
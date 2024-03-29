﻿@GUI
Feature: EcommerceCartCheckoutFunctionality
As a user
I want to be able to add an item to my cart 
So I can go through the checkout process 

Background: 
Given I am logged in to the ecommerce site

@CheckDiscount 
Scenario Outline: Checking Discount Applied
    Given I have added 'Tshirt' to my cart
	When I input the coupon '<coupon>'
	Then A discount of <discount(%)>% is applied to my cart
	  And  The order total updates accordingly
Examples:
| coupon    | discount(%)|
| nfocus    | 25         |
| edgewords | 15         | 

@CheckOrderNo
Scenario: Checking The Order Has Been Placed
    Given I have added 'Sunglasses' to my cart
	When I proceed to checkout
	  And Fill in my billing details with 
	      | Field      | Value             |
	      | FirstName  | Smith             |
	      | LastName   | Jones             |
	      | Company    |                   | 
	      | Street     | 1 Oxford Street   |
	      | City       | London            |
	      | County     |                   |
	      | Postcode   | W1B 3AG           |
	      | Phone      | 123456789         |
	      | Email      | smith@example.com |
	      | OrderNotes |                   |
	Then I can place my order
	  And Verify my order has been placed 
	
	#Company, County and OrderNotes are all optional fields
	
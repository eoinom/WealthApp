/// <reference types="cypress" />

import * as ctx from  '../../../../quasar.conf.js'
import Chance from 'chance'
const chance = new Chance()


describe('Login page tests', () => {

  const existing_email = "eoinom@gmail.com"
  const existing_pass = "pass1234"
  var countries = [ 'Australia', 'Canada', 'France', 'Germany', 'Ireland', 'Italy', 'Netherlands', 'Poland', 'Portugal', 'Spain', 'United Kingdomn', 'United States', 'New Zealand' ]
  var currencyCodes = [ 'AUD', 'CAD', 'EUR', 'GBP', 'PLN', 'USD', 'NZD' ]
  const new_email = chance.email()
  const new_pass = chance.string({ length: 7 })
  const new_fname = chance.first()
  const new_lname = chance.last()
  const new_country = chance.pickone(countries)
  const new_currency = chance.pickone(currencyCodes)

  beforeEach(() => {
    cy.visit('/')
  })

  it('.should() - assert that <title> is correct', () => {
    cy.title().should('include', 'WealthApp.io')
  })

  it('contains `Login`', () => {
    cy.contains('Login')
  })

  it('contains `Register`', () => {
    cy.contains('Register')
  })

  it('has logo', () => {
    cy.get('.authForm img')
      .should('have.id', 'logo-auth')
      .and('have.attr', 'src')
      .and('match', /^(img\/Logo.png)/);
  })

  it('blocks going to dashboard without logging in', () => {
    // cy.pause()
    cy.visit('/dashboard')
    cy.contains('Login')
  })

  
  it('logs in existing user', () => {
    // fill out login form
    cy.get('input[type=email]').type(existing_email)
    cy.get('input[type=password]').type(existing_pass)
    cy.get('button[type=submit]').click()

    // Assert welcome message
    cy.contains('Welcome back')
  })

  it('registers new user', () => {
    // go to register form
    cy.get('.q-tab.q-tab--inactive').click()

    // fill out register form
    cy.get('#register-firstname').type(new_fname)
    cy.get('#register-lastname').type(new_lname)
    cy.get('#register-email').type(new_email)
    cy.get('#register-password').type(new_pass)
    cy.get('#register-confirmPassword').type(new_pass)
    cy.get('#register-country').click()
    cy.contains(new_country).click()
    cy.get('#register-currency').click().click()    // why two clicks needed? 
    // cy.get('#register-currency').click()
    cy.wait(500)
    cy.contains(new_currency).click()
    cy.get('#register-agreeTerms .q-checkbox__check').click()
    
    // submit form
    cy.get('button[type=register]').click()

    // Assert welcome message
    cy.contains('Welcome')
  })

})

// describe('Home page tests', () => {
//   beforeEach(() => {
//     cy.visit('/');
//   });
//   it('has pretty background', () => {
//     cy.get('.landing-wrapper')
//       .should('have.css', 'background').and('match', /(".+(\/img\/background).+\.png)/);
//   });
//   it('has pretty logo', () => {
//     cy.get('.landing-wrapper img')
//       .should('have.class', 'logo-main')
//       .and('have.attr', 'src')
//       .and('match', /^(data:image\/svg\+xml).+/);
//   });
//   it('has very important information', () => {
//     cy.get('.instruction-wrapper')
//       .should('contain', 'SETUP INSTRUCTIONS')
//       .and('contain', 'Configure Authentication')
//       .and('contain', 'Database Configuration and CRUD operations')
//       .and('contain', 'Continuous Integration & Continuous Deployment CI/CD');
//   });
// });

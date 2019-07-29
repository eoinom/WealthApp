/// <reference types="cypress" />

import * as ctx from  '../../../../quasar.conf.js'
import Chance from 'chance'
const chance = new Chance()


describe('Navigation tests from dashboard', () => {

  const existing_email = "eoinom@gmail.com"
  const existing_pass = "pass1234"
  
  beforeEach(() => {
    // Login with custom method (using existing email & password)
    cy.login(existing_email, existing_pass)
    cy.wait(2000)
  })

  it('can visit Accounts from Dashboard on desktop', () => {
    cy.visit('/dashboard')    
    
    // Set desktop viewport (should have burger menu button)
    cy.viewport('macbook-15')
    cy.wait(200)
    
    cy.get('.menuBtn').click()
    cy.contains('Accounts').click()
    cy.location('pathname').should('eq', '/accounts')

    cy.go('back')
    cy.location('pathname').should('eq', '/dashboard')    
  })

  it('can visit Loans from Dashboard on desktop', () => {
    cy.visit('/dashboard')    
    
    // Set desktop viewport (should have burger menu button)
    cy.viewport('macbook-15')
    cy.wait(200)
    
    cy.get('.menuBtn').click()
    cy.contains('Loans').click()
    cy.location('pathname').should('eq', '/loans')

    cy.go('back')
    cy.location('pathname').should('eq', '/dashboard')    
  })

  it('can visit Accounts from Dashboard on mobile', () => {
    cy.visit('/dashboard')    
    
    // Set mobile viewport (should have tab nav menu)
    cy.viewport('iphone-6')
    cy.wait(1000)
    cy.get('footer').find('a').filter('.q-tab--inactive').first().children().find('i').should('contain', 'account')    
    cy.get('footer').find('a').filter('.q-tab--inactive').first().click()
    cy.location('pathname').should('eq', '/accounts')

    cy.go(-1)
    cy.location('pathname').should('eq', '/dashboard')   
  })

  it('can visit Loans from Dashboard on mobile', () => {
    cy.visit('/dashboard')    
    
    // Set mobile viewport (should have tab nav menu)
    cy.viewport('iphone-6')
    cy.wait(1000)
    cy.get('footer').find('a').filter('.q-tab--inactive').last().children().find('i').should('have.class', 'fas').and('have.class', 'fa-hand-holding-usd')
    cy.get('footer').find('a').filter('.q-tab--inactive').last().click()
    cy.location('pathname').should('eq', '/loans')

    cy.go(-1)
    cy.location('pathname').should('eq', '/dashboard')    
  })

  it('can visit Dashboard from Accounts on desktop', () => {
    cy.visit('/accounts')    
    
    // Set desktop viewport (should have burger menu button)
    cy.viewport('macbook-15')
    cy.wait(200)
    
    cy.get('.menuBtn').click()
    cy.contains('Dashboard').click()
    cy.location('pathname').should('eq', '/dashboard')

    cy.go('back')
    cy.location('pathname').should('eq', '/accounts')    
  })

  it('can visit Loans from Dashboard on mobile', () => {
    cy.visit('/accounts')    
    
    // Set mobile viewport (should have tab nav menu)
    cy.viewport('iphone-6')
    cy.wait(1000)
    cy.get('footer').find('a').filter('.q-tab--inactive').children().find('i').should('have.class', 'fas').and('have.class', 'fa-chart-bar')
    cy.get('footer').find('a').filter('.q-tab--inactive').first().click()
    cy.location('pathname').should('eq', '/dashboard')

    cy.go(-1)
    cy.location('pathname').should('eq', '/accounts')    
  })
  
});

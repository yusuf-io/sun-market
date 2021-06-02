/// <reference types="cypress" />

context('Sidebar links', () => {
  beforeEach(() => {
    cy.visit('/')
    cy.wait(5000)
    cy.get('header button .mdi-menu').click()
  })

  it('has 4 links', () => {
    cy.get('nav.v-navigation-drawer')
      .find('a')
      .should(($a) => {
        expect($a).to.have.length(4)
      })
  })

  it('goes to customers page', () => {
    cy.get('nav.v-navigation-drawer a[href="/customers"]').click()
    cy.get('h1').should(($el) =>
      expect($el.text().trim()).to.equal('Customers')
    )
  })

  it('goes to create invoice page', () => {
    cy.get('nav.v-navigation-drawer a[href="/create-invoice"]').click()

    cy.get('h1').should(($el) =>
      expect($el.text().trim()).to.equal('Create Invoice')
    )
  })

  it('goes to sales orders page', () => {
    cy.get('nav.v-navigation-drawer a[href="/orders"]').click()

    cy.get('h1').should(($el) =>
      expect($el.text().trim()).to.equal('Sales Orders')
    )
  })
})

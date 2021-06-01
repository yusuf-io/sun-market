import { mount, createLocalVue } from '@vue/test-utils'
import Inventory from '@/pages/inventory.vue'
import Vuetify from 'vuetify'

describe('Inventory', () => {
  let localVue
  let vuetify
  let wrapper

  beforeEach(() => {
    localVue = createLocalVue()
    vuetify = new Vuetify()
    wrapper = mount(Inventory, {
      localVue,
      vuetify,
    })
  })

  test('is a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy()
  })

  test('has heading Inventory', () => {
    const title = wrapper.find('h1')
    expect(title.text()).toBe('Inventory')
  })
})

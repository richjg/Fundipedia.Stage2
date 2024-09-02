import { describe, it, expect } from 'vitest'

import { mount } from '@vue/test-utils'
import Orders from '../Orders.vue'

describe('Orders', () => {
  it('renders properly', () => {
    
    const wrapper = mount(Orders, {  })

    //Assert
    const labelsText = wrapper.findAll('label').map(l => l.text());

    expect(labelsText.length).equals(4);
    expect(labelsText[0]).equals('Rush Order');
    expect(labelsText[1]).equals('New Customer');
    expect(labelsText[2]).equals('Large Order');
    expect(labelsText[3]).equals('Order Type');
  })
})

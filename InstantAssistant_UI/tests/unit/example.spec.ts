import { mount } from '@vue/test-utils'
import IntentEdit from '@/views/IntentEdit.vue'

describe('IntentEdit.vue', () => {
  it('renders folder view', () => {
    const mockRoute = {
      params: {
        id: 'Outbox'
      }
    }
    const wrapper = mount(IntentEdit, {
      global: {
        mocks: {
          $route: mockRoute
        }
      }
    })
    expect(wrapper.text()).toMatch('Explore UI Components')
  })
})

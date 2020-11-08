import Vue from 'vue'
import Router from 'vue-router'
import Hello from '@/components/Hello'
import MedicineRecords from '@/components/MedicineRecords'
import MedicineDetails from '@/components/MedicineDetails'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Hello',
      component: Hello
    },
    {
      path: '/medicine-records',
      name: 'MedicineRecords',
      component: MedicineRecords
    },
    {
      path: '/medicine-details',
      name: 'MedicineDetails',
      component: MedicineDetails
    }
  ]
})

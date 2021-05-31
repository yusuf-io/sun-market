import axios from 'axios'

export const fetchCustomers = async () => {
  const res = await axios.get(`${process.env.VUE_APP_API_URL}/customers`)
  return res.data
}

export const createCustomer = async (customer) => {
  const res = await axios.post(
    `${process.env.VUE_APP_API_URL}/customer`,
    customer
  )
  return res.data
}

export const deleteCustomer = async (customerId) => {
  const res = await axios.delete(
    `${process.env.VUE_APP_API_URL}/customers/${customerId}`
  )
  return res.data
}
export default {
  fetchCustomers,
  createCustomer,
  deleteCustomer,
}

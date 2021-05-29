import axios from 'axios'

export const fetchCustomers = async () => {
  const res = await axios.get(`${process.env.VUE_APP_API_URL}/customers`)
  return res.data
}
export default {
  fetchCustomers,
}

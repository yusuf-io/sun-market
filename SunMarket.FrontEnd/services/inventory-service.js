import axios from 'axios'

export const fetchProductInventories = async () => {
  const res = await axios.get(`${process.env.VUE_APP_API_URL}/inventory`)
  return res.data
}
export default {
  fetchProductInventories,
}

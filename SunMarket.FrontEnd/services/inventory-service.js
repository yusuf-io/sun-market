import axios from 'axios'

export const getCurrentInventory = async () => {
  const res = await axios.get(`${process.env.VUE_APP_API_URL}/inventory`)
  return res.data
}

export const getSnapshotHistory = async () => {
  const res = await axios.get(
    `${process.env.VUE_APP_API_URL}/inventory/snapshot`
  )
  return res.data
}

export const updateInventory = async (shipment) => {
  const res = await axios.patch(
    `${process.env.VUE_APP_API_URL}/inventory`,
    shipment
  )
  return res.data
}
export default {
  getCurrentInventory,
  getSnapshotHistory,
  updateInventory,
}

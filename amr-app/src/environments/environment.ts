
const { config } = require("../assets/configs/config.json");

export const environment = {
  production: false,
  baseServiceUrl: config.baseServiceUrl,
  hostIp: config.hostIp
};


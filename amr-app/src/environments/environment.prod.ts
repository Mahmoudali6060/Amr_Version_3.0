
const { config } = require("../assets/configs/config.json");

export const environment = {
  production: true,
  baseServiceUrl: config.baseServiceUrl,
  hostIp: config.hostIp
};


import http from 'k6/http';
import { check, sleep } from 'k6';
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';
import { randomItem } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';
import { randomString } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export const options = {
  stages: [
    { duration: '10s', target: 10 },
    { duration: '20s', target: 50 },
    { duration: '5s', target: 100 },
  ],
};

const domain = 'https://localhost:7274';

function buildCompaniesJsonData() {
  const randomCnpj = randomIntBetween(10000000000000, 99999999999999);
  const randomStreetNumber = randomIntBetween(1, 300);
  const randomPhoneNumber = randomIntBetween(10000000, 99999999);
  const randomLegalNature = randomItem(["EIRELI", "MEI", "LTDA"]);
  const randomMainActivity = randomItem([4399103, 4723700, 4774100, 5611203, 9492800]);
  const randomPostalCode = randomIntBetween(10000, 99999);
  const randomPartnerQualifications = randomItem([5, 10, 16, 22, 54]);
  const randomPartnerIds = randomItem([
    "012af8a2-8de0-45b7-b9c7-dcfdb3b491b3",
    "1a0592e2-71f0-48cc-8267-0f8d75fe0a5e",
    "9132b269-ff90-402d-88d0-2f9e7fdb312f",
    "c7baa255-6bfb-4d67-91da-fc1d858fdf00",
    "fd532165-d7fd-44bb-b19b-f8e2a1c2073f",
  ]);


  const jsonData = {
    cnpj: randomCnpj.toString(),
    name: "Company " + randomString(100),
    legalNature: randomLegalNature,
    mainActivityId: randomMainActivity,
    address: {
      postalCode: randomPostalCode.toString(),
      street: randomString(10),
      number: randomStreetNumber.toString(),
      complement: randomString(10),
      neighborhood: randomString(10),
      city: randomString(10),
      state: randomString(3),
      country: randomString(10)
    },
    partners: [
      {
        partnerId: randomPartnerIds,
        qualificationId: randomPartnerQualifications,
        joinedAt: "2022-01-01"
      }
    ],
    phones: [
      {
        countryCode: "11",
        number: randomPhoneNumber.toString()
      }
    ]
  };

  return jsonData;
}

export default function () {

  const jsonData = buildCompaniesJsonData();

  let res = http.post(domain + "/companies", JSON.stringify(jsonData), {
    headers: {
      'Content-Type': 'application/json',
    },
  });

  check(res, {
    'status was 201': (r) => r.status == 201
  });

  if (res.status == 400) {
    console.warn(res);
  }

  else if (res.status == 500) {
    console.error(res);
  }

  sleep(1);
}

'use client';
import { AuctionItem, CSVItem } from '@/models/auction-item';
import axios from 'axios';
import React from 'react';
import { v4 as uuidv4 } from 'uuid';
import FileUpload from './components/FileUpload';
import { DeviceGroup } from '@/models/device-group';

const AdminPage: React.FC = () => {
  const handleCSVUpload = (data: CSVItem[]) => {
    const group: DeviceGroup[] = [];
    let groupItem: DeviceGroup | null = null;

    for (const item of data) {
      const { nummer, model, seriennummer, price } = item;

      if (nummer) {
        if (!model?.length && !seriennummer?.length && !price?.toString().length) {
          if (groupItem) {
            group.push(groupItem);
          }
          groupItem = {
            id: uuidv4(),
            name: nummer ?? '',
            devices: []
          };
        } else {
          const parsedPrice =
            typeof price === 'number'
              ? price
              : parseFloat((price ?? '')?.replace('€', '').trim() ?? '0');
          groupItem?.devices.push({
            id: uuidv4(),
            name: nummer ?? '',
            model: model ?? '',
            serialNumber: seriennummer ?? '',
            price: parsedPrice
          });
        }
      }
    }

    if (groupItem && groupItem.devices.length > 0) {
      group.push(groupItem);
    }

    console.log({
      group
    });

    group.forEach((item) => {
      axios
        .post('http://localhost:5156/api/device-groups', item)
        .then((data) => {
          console.log('data', data);
        })
        .catch((error) => {
          console.error(error);
        });
    });
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="mb-4 text-2xl font-semibold">Admin Dashboard</h1>
      <FileUpload onUpload={handleCSVUpload} />
      {/* ... (rest of the code) */}
    </div>
  );
};

export default AdminPage;

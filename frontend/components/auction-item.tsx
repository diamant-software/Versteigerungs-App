'use client';
import { AuctionItem } from '@/models/auction-item';
import { Modal } from './modal';
import { useState } from 'react';
import axios from 'axios';

const AuctionItemComponent: React.FC<{ item: AuctionItem }> = ({ item }) => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const openModal = () => {
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setIsModalOpen(false);
  };

  const updateValue = (newValue: number) => {
    const deviceId = item.id;
    axios
      .patch(
        `http://localhost:5156/api/bid/${deviceId}`,
        {
          price: newValue
        },
        {
          withCredentials: true
        }
      )
      .then((data) => {
        console.log('update', data);
      })
      .catch((error) => {
        console.error('update error', error);
      });
  };

  return (
    <div className="flex flex-col justify-between rounded-lg bg-white p-6 shadow-md">
      <h2 className="mb-2 text-xl font-semibold">{item.model}</h2>
      <p className="mb-1 text-gray-600">Name: {item.name}</p>
      <p className="mb-1 text-gray-600">Serial Number: {item.serialNumber}</p>
      <p className="text-blue-500">Preis: {item.price} €</p>
      <button onClick={openModal} className="mt-2 rounded bg-blue-500 px-4 py-2 text-white">
        Bieten
      </button>
      <Modal
        isOpen={isModalOpen}
        onClose={closeModal}
        currentValue={item.price}
        onUpdateValue={updateValue}
      />
    </div>
  );
};

export { AuctionItemComponent };

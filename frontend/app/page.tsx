'use client';
import { AuctionListComponent } from '@/components/auction-list';
import { LoginButton } from '@/components/login-button';
import { DeviceGroup } from '@/models/device-group';
import { AuthenticatedTemplate, UnauthenticatedTemplate } from '@azure/msal-react';
import axios from 'axios';
import Link from 'next/link';
import React, { useEffect, useState } from 'react';

const RemovableList: React.FC<{ items: { id: string; name: string }[] }> = ({ items }) => {
  const [itemList, setItemList] = useState(items);

  const handleRemove = (itemId: string) => {
    const updatedList = itemList.filter((item) => item.id !== itemId);
    setItemList(updatedList);
  };

  return (
    <div className="grid grid-cols-3 gap-4">
      {itemList.map((item) => (
        <div
          key={item.id}
          className="relative rounded-md border border-gray-300 p-4 transition-all hover:shadow-md"
        >
          {item.name}
          <button
            onClick={() => handleRemove(item.id)}
            className="absolute right-1 top-1 rounded-full bg-red-500 px-2 py-1 text-white transition-colors hover:bg-red-600"
          >
            Remove
          </button>
        </div>
      ))}
    </div>
  );
};

const HomePage: React.FC = () => {
  const [auctionItems, setAuctionItems] = useState<DeviceGroup[]>();

  const [filterGroup, setFilterGroup] = useState<string>('');

  useEffect(() => {
    axios
      .get<DeviceGroup[]>(`http://localhost:5156/api/device-groups`, { withCredentials: true })
      .then((data) => {
        setAuctionItems(data.data);
      });
  }, []);

  return (
    <div className="container mx-auto p-4">
      <h1 className="mb-4 text-2xl font-semibold">Diamant Verteigerungs Platform</h1>

      <AuthenticatedTemplate>
        <Link className="mb-4 rounded-md" href="/profile">
          Request Profile Information
        </Link>
      </AuthenticatedTemplate>

      <UnauthenticatedTemplate>
        <p>Please sign-in to see your profile information.</p>
        <LoginButton />
      </UnauthenticatedTemplate>

      <div className="flex justify-start gap-4 pb-4">
        {auctionItems?.map((item) => {
          return (
            <div
              key={item.id}
              onClick={() => {
                if (filterGroup === item.name) {
                  setFilterGroup('');
                } else {
                  setFilterGroup(item.name);
                }
              }}
              className={
                'cursor-pointer transition duration-300 ease-in-out ' +
                (filterGroup === item.name ? 'text-indigo-400' : '')
              }
            >
              {item.name}
            </div>
          );
        })}
      </div>

      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4">
        {auctionItems
          ?.filter((group) => {
            if (!filterGroup) {
              return true;
            }

            return group.name === filterGroup;
          })
          ?.map((item) => <AuctionListComponent key={item.id} items={item.devices} />)}
      </div>
    </div>
  );
};

export default HomePage;

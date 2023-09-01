'use client';
import { AuctionListComponent } from '@/components/auction-list';
import { LoginButton } from '@/components/login-button';
import { LogoutButton } from '@/components/logout-button';
import api from '@/components/api';
import { DeviceGroup } from '@/models/device-group';
import { AuthenticatedTemplate, UnauthenticatedTemplate, useIsAuthenticated, useMsal } from '@azure/msal-react';
import React, { useEffect, useState } from 'react';

const HomePage: React.FC = () => {
  const [auctionItems, setAuctionItems] = useState<DeviceGroup[]>();
  const [filterGroup, setFilterGroup] = useState<string>('');
  const isAuthenticated = useIsAuthenticated();

  useEffect(() => {
    if (isAuthenticated) {
      api.get<DeviceGroup[]>(`device-groups`).then((data) => {
        setAuctionItems(data.data);
      });
    }
  }, [isAuthenticated]);

  return (
    <div className="container mx-auto p-4">
      <h1 className="mb-4 text-2xl font-semibold">Diamant Verteigerungs Platform</h1>

      <AuthenticatedTemplate>
        <LogoutButton />

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
      </AuthenticatedTemplate>

      <UnauthenticatedTemplate>
        <p>Bitte loggen sie sich ein.</p>
        <LoginButton />
      </UnauthenticatedTemplate>
    </div>
  );
};

export default HomePage;
